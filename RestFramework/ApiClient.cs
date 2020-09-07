using CommonClasses;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace RestFramework {
  internal class Todos {
    public List<TodoItemData> todos;
  }

  public class ApiClient {
    string _baseUrl;

    public ApiClient(string baseUrl) {
      _baseUrl = baseUrl;
    }

    public List<TodoItemData> GetAll() {
      string endpoint = "/api/todo";

      RestClient client = new RestClient(_baseUrl);
      RestRequest request = new RestRequest(endpoint, Method.GET);

      IRestResponse response = client.Execute(request);

      if (response.StatusCode != HttpStatusCode.OK) {
        throw new WebException(response.ErrorMessage);
      }

      // handle response 
      var todos = JsonConvert.DeserializeObject<Todos>(response.Content);
      var items = todos.todos;

      return items;
    }

    public bool AddNew(TodoItemData newItem) {
      string endpoint = "/api/todo";

      var body = JsonConvert.SerializeObject(newItem);

      var client = new RestClient(_baseUrl);
      var request = new RestRequest(endpoint, Method.POST)
          .AddJsonBody(body);

      IRestResponse response = client.Execute(request);

      return response.StatusCode == HttpStatusCode.OK;
    }

    public bool CompleteItem(int id) {
      string endpoint = $"/api/todo/{id}";

      var client = new RestClient(_baseUrl);
      var request = new RestRequest(endpoint, Method.PUT);

      IRestResponse response = client.Execute(request);
      return response.StatusCode == HttpStatusCode.OK;
    }

    public bool DeleteItem(int id) {
      string endpoint = $"/api/todo/{id}";

      var client = new RestClient(_baseUrl);
      var request = new RestRequest(endpoint, Method.DELETE);

      IRestResponse response = client.Execute(request);
      return response.StatusCode == HttpStatusCode.OK;
    }
  }
}
