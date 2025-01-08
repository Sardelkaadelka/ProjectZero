class Program
{
  static void Main()
  {
    User[] users = [];
    int port = 5000;
     int userindex=-1;
    
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

            User newUser = new User(username, password);
            users = [.. users, newUser];
            Console.WriteLine(username + "," + password);

            response.Send(newUser.id);
          }
          else if (request.Path == "login")
          {
            (string username, string password) = request.GetBody<(string, string)>();
            bool FoundUser = false;
            string?userId = null;
            for (int i = 0; i < users.Length; i++)
            {
              if (username == users[i].username
               && password == users[i].password)
              {
                FoundUser = true;
                userId = users[i].id;
                userindex=i;
                
              }
            }
            response.Send((FoundUser, userId));
          }
          else if (request.Path == "FoundUser")
          {
            string userId = request.GetBody<string>();

            bool FoundUser = false;
            for (int i = 0; i < users.Length; i++)
            {
              if (userId == users[i].id)
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
            for (int i = 0; i < users.Length; i++)
            {
              if (users[i].id == userId)
              {
                username = users[i].username;
              }
            }
            response.Send(username);
          }
         else if(request.Path=="addtocart"){
            var (userId, picId) = request.GetBody<(string, int)>();
            User myUser = default!;
            for (int i=0; i<users.Length;i++){
              if (users[i].id==userId){
                myUser = users[i];
              }
            }
            myUser.favorites[picId] = true;
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
  class User{
    public string username;
    public string password;
    public string id;

    public bool[] favorites;

    public User(string username,string password){
      this.username = username;
      this.password = password;
      id = new Guid().ToString();
      favorites = [false,false,false];
    }
      }
}