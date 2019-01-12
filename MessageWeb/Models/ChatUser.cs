using System;

namespace MessageWeb.Models
{
	public class ChatUser : IBaseApiModels
	{
		public long Starttime { get; set; }
		public long endtime { get; set; }
		public bool Isadmin { get; set; }

		public ChatData ChatData = null;
		//Todo
		public object SelfCreatByJson(object[] Json)
		{
			throw new System.NotImplementedException();
		}
	}
}