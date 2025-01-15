class Program
{
  static void Main()
  {
    User[] users = [];
    int port = 5000;
    
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
            string?UserID = null;
            for (int i = 0; i < users.Length; i++)
            {
              if (username == users[i].username
               && password == users[i].password)
              {
                FoundUser = true;
                UserID= users[i].id;
                
              }
            }
            response.Send((FoundUser, UserID));
          }
          else if (request.Path == "FoundUser")
          {
            string UserID = request.GetBody<string>();

            bool FoundUser = false;
            for (int i = 0; i < users.Length; i++)
            {
              if (UserID == users[i].id)
              {
                FoundUser = true;
              }
            }

            response.Send(FoundUser);
          }
          else if (request.Path == "GetUserName")
          {
            string UserID = request.GetBody<string>();
            string username = "";
            for (int i = 0; i < users.Length; i++)
            {
              if (users[i].id == UserID)
              {
                username = users[i].username;
              }
            }
            response.Send(username);
          }
         else if(request.Path=="addtocart"){
            (int i, string UserID) = request.GetBody<(int, string)>();
            User myUser = default!;
            for (int j=0; j<users.Length;j++){
              if (users[j].id==UserID){
                myUser = users[j];
              }
            }
            myUser.favorites[i] = true;

         }
         else if(request.Path=="removefromcart"){
           (int i, string UserID) = request.GetBody<(int, string)>();
            User myUser = default!;
            for (int j = 0; j < users.Length; j++)
            {
              if (users[j].id == UserID)
              {
                myUser = users[i];
              }
            }
            myUser.favorites[i] = false;

          }
          else if (request.Path == "getFavorites")
          {
            string UserID = request.GetBody<string>();
            User myUser = default!;
            for (int j = 0; j < users.Length; j++)
            {
              if (users[j].id == UserID)
              {
                myUser = users[j];
              }
            }
            response.Send(myUser.favorites);
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
class User{
    public string username;
    public string password;
    public string id;

    public bool[] favorites;

    public User(string username,string password){
      this.username = username;
      this.password = password;
      id = Guid.NewGuid().ToString();
      favorites = [false,false,false];
    }
      
}