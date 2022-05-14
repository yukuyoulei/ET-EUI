namespace ET
{
	public  class DlgPlay :Entity,IAwake,IUILogic
	{

		public DlgPlayViewComponent View { get => this.Parent.GetComponent<DlgPlayViewComponent>();} 

		 

	}
}
