using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace ET
{
	[FriendClass(typeof(DBComponent))]
    public static class DBComponentSystem
    {
	    public class DBComponentAwakeSystem : AwakeSystem<DBComponent, string, string, int>
	    {
		    public override void Awake(DBComponent me, string dbConnection, string dbName, int zone)
		    {
			    me.mongoClient = new MongoClient(dbConnection);
			    me.database = me.mongoClient.GetDatabase(dbName);
		    }
	    }

	    private static IMongoCollection<T> GetCollection<T>(this DBComponent me, string collection = null)
	    {
		    return me.database.GetCollection<T>(collection ?? typeof (T).Name);
	    }

	    private static IMongoCollection<Entity> GetCollection(this DBComponent me, string name)
	    {
		    return me.database.GetCollection<Entity>(name);
	    }
	    
	    #region Query

	    public static async ETTask<T> Query<T>(this DBComponent me, long id, string collection = null) where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
		    {
			    IAsyncCursor<T> cursor = await me.GetCollection<T>(collection).FindAsync(d => d.Id == id);

			    return await cursor.FirstOrDefaultAsync();
		    }
	    }
	    
	    public static async ETTask<List<T>> Query<T>(this DBComponent me, Expression<Func<T, bool>> filter, string collection = null)
			    where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
		    {
			    IAsyncCursor<T> cursor = await me.GetCollection<T>(collection).FindAsync(filter);

			    return await cursor.ToListAsync();
		    }
	    }

	    public static async ETTask<List<T>> Query<T>(this DBComponent me, long taskId, Expression<Func<T, bool>> filter, string collection = null)
			    where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
		    {
			    IAsyncCursor<T> cursor = await me.GetCollection<T>(collection).FindAsync(filter);

			    return await cursor.ToListAsync();
		    }
	    }
	    
	    public static async ETTask Query(this DBComponent me, long id, List<string> collectionNames, List<Entity> result)
	    {
		    if (collectionNames == null || collectionNames.Count == 0)
		    {
			    return;
		    }

		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
		    {
			    foreach (string collectionName in collectionNames)
			    {
				    IAsyncCursor<Entity> cursor = await me.GetCollection(collectionName).FindAsync(d => d.Id == id);

				    Entity e = await cursor.FirstOrDefaultAsync();

				    if (e == null)
				    {
					    continue;
				    }

				    result.Add(e);
			    }
		    }
	    }

	    public static async ETTask<List<T>> QueryJson<T>(this DBComponent me, string json, string collection = null) where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
		    {
			    FilterDefinition<T> filterDefinition = new JsonFilterDefinition<T>(json);
			    IAsyncCursor<T> cursor = await me.GetCollection<T>(collection).FindAsync(filterDefinition);
			    return await cursor.ToListAsync();
		    }
	    }

	    public static async ETTask<List<T>> QueryJson<T>(this DBComponent me, long taskId, string json, string collection = null) where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
		    {
			    FilterDefinition<T> filterDefinition = new JsonFilterDefinition<T>(json);
			    IAsyncCursor<T> cursor = await me.GetCollection<T>(collection).FindAsync(filterDefinition);
			    return await cursor.ToListAsync();
		    }
	    }

	    #endregion

	    #region Insert

	    public static async ETTask InsertBatch<T>(this DBComponent me, IEnumerable<T> list, string collection = null) where T: Entity
	    {
		    if (collection == null)
		    {
			    collection = typeof (T).Name;
		    }
		    
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
		    {
			    await me.GetCollection(collection).InsertManyAsync(list);
		    }
	    }

	    #endregion

	    #region Save

	    public static async ETTask Save<T>(this DBComponent me, T entity, string collection = null) where T : Entity
	    {
		    if (entity == null)
		    {
			    Log.Error($"save entity is null: {typeof (T).Name}");

			    return;
		    }
		    
		    if (collection == null)
		    {
			    collection = entity.GetType().Name;
		    }

		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, entity.Id % DBComponent.TaskCount))
		    {
			    await me.GetCollection(collection).ReplaceOneAsync(d => d.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
		    }
	    }

	    public static async ETTask Save<T>(this DBComponent me, long taskId, T entity, string collection = null) where T : Entity
	    {
		    if (entity == null)
		    {
			    Log.Error($"save entity is null: {typeof (T).Name}");

			    return;
		    }

		    if (collection == null)
		    {
			    collection = entity.GetType().Name;
		    }

		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
		    {
			    await me.GetCollection(collection).ReplaceOneAsync(d => d.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
		    }
	    }

	    public static async ETTask Save(this DBComponent me, long id, List<Entity> entities)
	    {
		    if (entities == null)
		    {
			    Log.Error($"save entity is null");
			    return;
		    }

		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
		    {
			    foreach (Entity entity in entities)
			    {
				    if (entity == null)
				    {
					    continue;
				    }

				    await me.GetCollection(entity.GetType().Name)
						    .ReplaceOneAsync(d => d.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
			    }
		    }
	    }

	    public static async ETTask SaveNotWait<T>(this DBComponent me, T entity, long taskId = 0, string collection = null) where T : Entity
	    {
		    if (taskId == 0)
		    {
			    await me.Save(entity, collection);

			    return;
		    }

		    await me.Save(taskId, entity, collection);
	    }

	    #endregion

	    #region Remove
	    
	    public static async ETTask<long> Remove<T>(this DBComponent me, long id, string collection = null) where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, id % DBComponent.TaskCount))
		    {
			    DeleteResult result = await me.GetCollection<T>(collection).DeleteOneAsync(d => d.Id == id);

			    return result.DeletedCount;
		    }
	    }

	    public static async ETTask<long> Remove<T>(this DBComponent me, long taskId, long id, string collection = null) where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
		    {
			    DeleteResult result = await me.GetCollection<T>(collection).DeleteOneAsync(d => d.Id == id);

			    return result.DeletedCount;
		    }
	    }

	    public static async ETTask<long> Remove<T>(this DBComponent me, Expression<Func<T, bool>> filter, string collection = null) where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, RandomHelper.RandInt64() % DBComponent.TaskCount))
		    {
			    DeleteResult result = await me.GetCollection<T>(collection).DeleteManyAsync(filter);

			    return result.DeletedCount;
		    }
	    }

	    public static async ETTask<long> Remove<T>(this DBComponent me, long taskId, Expression<Func<T, bool>> filter, string collection = null)
			    where T : Entity
	    {
		    using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DB, taskId % DBComponent.TaskCount))
		    {
			    DeleteResult result = await me.GetCollection<T>(collection).DeleteManyAsync(filter);

			    return result.DeletedCount;
		    }
	    }

	    #endregion
    }
}