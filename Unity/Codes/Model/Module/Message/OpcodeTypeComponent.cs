using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(OpcodeTypeComponent))]
    public static class OpcodeTypeComponentSystem
    {
        [ObjectSystem]
        public class OpcodeTypeComponentAwakeSystem: AwakeSystem<OpcodeTypeComponent>
        {
            public override void Awake(OpcodeTypeComponent me)
            {
                OpcodeTypeComponent.Instance = me;
                
                me.opcodeTypes.Clear();
                me.typeOpcodes.Clear();
                me.requestResponse.Clear();

                List<Type> types = Game.EventSystem.GetTypes(typeof (MessageAttribute));
                foreach (Type type in types)
                {
                    object[] attrs = type.GetCustomAttributes(typeof (MessageAttribute), false);
                    if (attrs.Length == 0)
                    {
                        continue;
                    }

                    MessageAttribute messageAttribute = attrs[0] as MessageAttribute;
                    if (messageAttribute == null)
                    {
                        continue;
                    }
                

                    me.opcodeTypes.Add(messageAttribute.Opcode, type);
                    me.typeOpcodes.Add(type, messageAttribute.Opcode);

                    if (OpcodeHelper.IsOuterMessage(messageAttribute.Opcode) && typeof (IActorMessage).IsAssignableFrom(type))
                    {
                        me.outrActorMessage.Add(messageAttribute.Opcode);
                    }
                
                    // 检查request response
                    if (typeof (IRequest).IsAssignableFrom(type))
                    {
                        if (typeof (IActorLocationMessage).IsAssignableFrom(type))
                        {
                            me.requestResponse.Add(type, typeof(ActorResponse));
                            continue;
                        }
                    
                        attrs = type.GetCustomAttributes(typeof (ResponseTypeAttribute), false);
                        if (attrs.Length == 0)
                        {
                            Log.Error($"not found responseType: {type}");
                            continue;
                        }

                        ResponseTypeAttribute responseTypeAttribute = attrs[0] as ResponseTypeAttribute;
                        me.requestResponse.Add(type, Game.EventSystem.GetType($"ET.{responseTypeAttribute.Type}"));
                    }
                }
            }
        }

        [ObjectSystem]
        public class OpcodeTypeComponentDestroySystem: DestroySystem<OpcodeTypeComponent>
        {
            public override void Destroy(OpcodeTypeComponent me)
            {
                OpcodeTypeComponent.Instance = null;
            }
        }

        public static bool IsOutrActorMessage(this OpcodeTypeComponent me, ushort opcode)
        {
            return me.outrActorMessage.Contains(opcode);
        }

        public static ushort GetOpcode(this OpcodeTypeComponent me, Type type)
        {
            return me.typeOpcodes[type];
        }

        public static Type GetType(this OpcodeTypeComponent me, ushort opcode)
        {
            return me.opcodeTypes[opcode];
        }

        public static Type GetResponseType(this OpcodeTypeComponent me, Type request)
        {
            if (!me.requestResponse.TryGetValue(request, out Type response))
            {
                throw new Exception($"not found response type, request type: {request.GetType().Name}");
            }
            return response;
        }
    }

    public class OpcodeTypeComponent: Entity, IAwake, IDestroy
    {
        public static OpcodeTypeComponent Instance;
        
        public HashSet<ushort> outrActorMessage = new HashSet<ushort>();
        
        public readonly Dictionary<ushort, Type> opcodeTypes = new Dictionary<ushort, Type>();
        public readonly Dictionary<Type, ushort> typeOpcodes = new Dictionary<Type, ushort>();
        
        public readonly Dictionary<Type, Type> requestResponse = new Dictionary<Type, Type>();
    }
}