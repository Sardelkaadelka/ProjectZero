class Program
{
  static void Main()
  {
    int port = 5000;
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
            usernames = [.. usernames, username];
            passwords = [.. passwords, password];
            ids = [.. ids, Guid.NewGuid().ToString()];
            Console.WriteLine(username + "," + password);
          }
          else if (request.Path == "login")
          {
            (string username, string password) = request.GetBody<(string, string)>();
            bool FoundUser = false;
            string UserID = "";
            for (int i = 0; i < usernames.Length; i++)
            {
              if (username == usernames[i] && password = passwords[i])
              {
                FoundUser = true;
                UserID = ids[i];
              }
            }
            response.Send((FoundUser, UserID));
          }
          else if (request.Path == "GetUserName")
          {
            string UserID = request.GetBody<string>();
            int i = 0;
            while (ids[i] != UserID)
            {
              i++;
            }
            string username = usernames[i];
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