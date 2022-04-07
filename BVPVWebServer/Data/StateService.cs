namespace BVPVWebServer.Data;

public class StateService
{
    public class UserState
    {
        public UserState Info;
        public UserSystemState SystemState;
        public UserAppState AppState;

        public void Get()
        {
            
        }

        public void UpdateAll()
        {
            
        }

        public void UpdateSystemState()
        {
            
        }

        public void UpdateAppState()
        {
            
        }

        public void Delete()
        {
            
        }
        
    }
}

public class UserStateInfo
{
    public string User { get; set; }
    public List<UserAppState> AppStates { get; set; }
    public UserSystemState SystemState { get; set; }
}

public class UserSystemState
{
    public bool DarkTheme { get; set; }
}

public class UserAppState
{
    public string App { get; set; }
    public string Setting { get; set; }
}