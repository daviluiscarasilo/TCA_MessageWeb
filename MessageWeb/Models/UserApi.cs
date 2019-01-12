using Newtonsoft.Json.Linq;
using System;

namespace MessageWeb.Models
{
	public class UserApi : IBaseApiModels
	{
		public long UserId { get; set; }
		public long ExtensionId { get; set; }

		public bool LoginOk { get; set; }

		public UserApi(long UserId, long ExtensionId,bool Loginok)
		{
			this.UserId = UserId;
			this.ExtensionId = ExtensionId;
			this.LoginOk = Loginok;
		}
		public UserApi()
		{}

		public object SelfCreatByJson(object[] Json)
		{
			JObject jObject = JObject.Parse(Json[0].ToString());

			return new UserApi(Int64.Parse(jObject[nameof(UserId).ToLower()].ToString()),
								Int64.Parse(jObject[nameof(ExtensionId).ToLower()].ToString()),
								Convert.ToBoolean(jObject[nameof(LoginOk).ToLower()].ToString()));
		}

	}
}