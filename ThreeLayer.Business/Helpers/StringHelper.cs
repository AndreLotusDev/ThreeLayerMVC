using ThreeLayer.Business.Enum;

namespace ThreeLayer.Business.Helpers
{
    public class StringHelper
    {
        public static string TranslateEnum(EReturnMessageOperation eReturnMessageOperation)
        {
            switch (eReturnMessageOperation)
            {
                case EReturnMessageOperation.SUCCESSFULLY_ADDED:
                    return "Successfully added!";
                case EReturnMessageOperation.SUCCESSFULLY_UPDATED:
                    return "Successfully updated!";
                case EReturnMessageOperation.SUCCESSFULLY_DELETED:
                    return "Successfully deleted!";
                case EReturnMessageOperation.SUCCESSFULLY_EDITED:
                    return "Successfully edited!";
                case EReturnMessageOperation.SUCCESSFULLY_UPLOADED:
                    return "Successfully uploaded!";
                case EReturnMessageOperation.SOMETHING_WENT_WRONG:
                    return "Something went wrong!";
                case EReturnMessageOperation.FOUND:
                    return "Found!";
                case EReturnMessageOperation.NOT_FOUND:
                    return "Not Found!";
                default:
                    return String.Empty;
            }
        }
    }
}
