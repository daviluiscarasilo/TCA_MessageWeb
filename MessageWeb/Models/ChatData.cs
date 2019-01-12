using System;
using System.Collections.Generic;

namespace MessageWeb.Models
{
	public class ChatData : IBaseApiModels
	{
		/// <summary>
		/// chatdid
		/// </summary>
		public string id { get; set; }
		public string title { get; set; }
		public string Description { get; set; }
		public int Id_creator { get; set; }
		public bool Is_active { get; set; }
		public bool Is_pro { get; set; }
		public Enuns.Daysofweek Daysofweek { get; set; }
		public int startHour { get; set; }
		public int endHour { get; set; }
		public int Startminute { get; set; }
		public int endminute { get; set; }
		public string chat_active { get; set; }
		public List<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
		//Todo
		public object SelfCreatByJson(object[] Json)
		{
			throw new System.NotImplementedException();
		}
	}
}