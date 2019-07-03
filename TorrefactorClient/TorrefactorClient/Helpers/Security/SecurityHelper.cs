using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TorrefactorClient.Helpers.Security
{
  public static class SecurityHelper
  {
    public static string convertToUNSecureString(SecureString secstrPassword)
    {
      IntPtr unmanagedString = IntPtr.Zero;
      try
      {
        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
        return Marshal.PtrToStringUni(unmanagedString);
      }
      finally
      {
        Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
      }
    }
  }
}
