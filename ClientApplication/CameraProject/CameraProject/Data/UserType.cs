namespace CameraProject.Data
{
    public enum UserType
    {
        ANONYMOUS, ADMIN
    }

    class UserTypeClass
    {
        /*private string[][] adminPermArray = new string[2][] { {"admin","admin"},
                                                              {"admin2","admin2"} };*/

        string[][] adminPermArray =  {
                    new string[] {"admin","admin"},
                    new string[] {"admin2","admin2"}
                                            };

        public bool isAuthorized( string userNameP, string passwordP )
        {
            for( int i = 0; i < adminPermArray.Length; i++ )
            {
                if( adminPermArray[i][0].Equals(userNameP) && adminPermArray[i][1].Equals(userNameP) )
                {
                    return true;
                }
            }
            return false;
        }

    }


}