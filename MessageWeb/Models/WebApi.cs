using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace MessageWeb.Models
{
	public class WebApi
	{
		public string Token { get; } = WebConfigurationManager.AppSettings["ApiToken"];
		public string UrlBase { get; } = WebConfigurationManager.AppSettings["ApiUrlBase"];

		public Dictionary<string, string> WebParamaters = null;

		private readonly HttpClient client = null;


		public WebApi()
		{
			client = new HttpClient();
		}

		/// <summary>
		/// Method used for make post request,
		/// The api url is setted by UrlBase property
		/// </summary>
		/// <param name="Method"></param>
		/// <returns></returns>
		private string PostApi(string Method)
		{
			var content = new FormUrlEncodedContent(WebParamaters);

			var response = client.PostAsync(string.Concat(UrlBase, Method), content).Result;

			if (response.IsSuccessStatusCode)
			{
				var responseContent = response.Content;

				string responseString = responseContent.ReadAsStringAsync().Result;
				if (responseString != null)
					return responseString;
			}
			
			return String.Empty;
		}

		/// <summary>
		/// Methor to create a generic object by reflection 
		/// </summary>
		/// <param name="Json"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		private object CreateObjectByJson(string Json,Type type)
		{
			MethodInfo genericMethod = type.GetMethod("SelfCreatByJson")
				.MakeGenericMethod(typeof(string));

			object[] paramater = new object[1];

			paramater[0] = new { Json };

			return genericMethod.Invoke(this, paramater);
		}
		////Todo
		///// <summary>
		///// Methor to create a generic object by reflection 
		///// </summary>
		///// <param name="Json"></param>
		///// <param name="type"></param>
		///// <returns></returns>
		//private List<object> CreateObjectByJson(List<string> Json, Type type)
		//{
		//	MethodInfo genericMethod = type.GetMethod("SelfCreatByJson")
		//		.MakeGenericMethod(typeof(string));

		//	object[] paramater = new object[1];

		//	paramater[0] = new { Json };

		//	return genericMethod.Invoke(this, paramater);
		//}
		public UserApi LoginVerify(string Username, string Password)
		{
			WebParamaters = new Dictionary<string, string>
							{
							   { "token", Token },
							   { "username", Username },
							   { "password", Password }
							};
			var response = PostApi(String.Concat(nameof(LoginVerify).ToLower(),".php"));
			//Incorrect user/ Error in post request
			if (String.IsNullOrEmpty(response) || response.Contains("loginincorrect"))
				return null;
			else
			{
				return (UserApi)CreateObjectByJson(response,typeof(UserApi));
			}
		}

		//Todo
		public List<ChatData> DownChats(string Username,string Password)
		{
			WebParamaters = new Dictionary<string, string>
							{
							   { "token", Token },
							   { "username", Username },
							   { "password", Password },
							   { "command", "syncdownchats" }
							};
			var response = PostApi(String.Concat(nameof(DownChats).ToLower(), ".php"));

			if (String.IsNullOrEmpty(response))
				return null;
			else
			{
				JObject jObject = JObject.Parse(response);

				var ChatDatas = jObject["CHATDATA"].ToArray();

				return (List<ChatData>)CreateObjectByJson(response, typeof(ChatData));
			}
		}





	}
}