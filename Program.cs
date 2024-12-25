class Program
{
  static void Main()
  {
    int port = 5000;
     int userindex=-1;
    string[] usernames = [];
    string[] passwords = [];
    string[] ids=[];
    var server = new Server(port);

    Console.WriteLine("The server is running");
    Console.WriteLine($"Main Page: http://localhost:{port}/website/pages/index.html");

    while (true)
    {
      (var request, var response) = server.WaitForRequest();

      Console.WriteLine($"Recieved a request with the path: {request.Path}");

      if (File.Exists(request.Path))
      {
        var file = new File(request.Path);
        response.Send(file);
      }
      else if (request.ExpectsHtml())
      {
        var file = new File("website/pages/404.html");
        response.SetStatusCode(404);
        response.Send(file);
      }

      else
      {
        try
        {
          /*──────────────────────────────────╮
          │ Handle your custome requests here │
          ╰──────────────────────────────────*/
          if (request.Path == "signup")
          {
            (string username, string password) = request.GetBody<(string, string)>();
            string userId = Guid.NewGuid().ToString();

            usernames = [.. usernames, username];
            passwords = [.. passwords, password];
            ids = [.. ids, userId];
            Console.WriteLine(username + "," + password);

            response.Send(userId);
          }
          else if (request.Path == "login")
          {
            (string username, string password) = request.GetBody<(string, string)>();
            bool FoundUser = false;
            string?userId = null;
            for (int i = 0; i < ids.Length; i++)
            {
              if (username == usernames[i] && password == passwords[i])
              {
                FoundUser = true;
                userId = ids[i];
                userindex=i;
                
              }
            }
            response.Send((FoundUser, userId));
          }
          else if (request.Path == "FoundUser")
          {
            string userId = request.GetBody<string>();

            bool FoundUser = false;
            for (int i = 0; i < ids.Length; i++)
            {
              if (userId == ids[i])
              {
                FoundUser = true;
              }
            }

            response.Send(FoundUser);
          }
          else if (request.Path == "GetUserName")
          {
            string userId = request.GetBody<string>();
            string username = "";
            for (int i = 0; i < ids.Length; i++)
            {
              if (ids[i] == userId)
              {
                username = usernames[i];
              }
            }
            response.Send(username);
          }
          else
          {
            response.SetStatusCode(405);
          }
        }
        catch (Exception exception)
        {
          Log.WriteException(exception);
        }
      }

      response.Close();
    }
  }
}