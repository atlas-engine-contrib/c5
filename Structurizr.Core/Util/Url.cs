using System;

namespace AtlasEngine.Modelling.C5.Util
{

    public class Url
    {

        public static bool IsUrl(string urlAsString)
        {
            if (urlAsString != null && urlAsString.Trim().Length > 0)
            {
                Uri uri;
                return Uri.TryCreate(urlAsString, UriKind.Absolute, out uri);
            }

            return false;
        }

    }

}
