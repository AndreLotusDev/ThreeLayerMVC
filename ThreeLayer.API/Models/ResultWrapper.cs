using ThreeLayer.Business.Enum;
using ThreeLayer.Business.Helpers;

namespace ThreeLayer.API.Models
{
    public class ResultWrapper<T>
    {
        public ResultWrapper(T data, bool status, List<string> messages, string uiMessage)
        {
            Messages = messages;
            Status = status;
            Data = data;
            UiMessage = uiMessage;
        }
        public List<string> Messages { get; set; }
        public string UiMessage { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }

        public static ResultWrapper<T> FactoryFoundOrNot(T data, bool status, List<string> messages) => new ResultWrapper<T>(
                data,
                status,
                messages,
                status ?
                    StringHelper.TranslateEnum(EReturnMessageOperation.FOUND) :
                    StringHelper.TranslateEnum(EReturnMessageOperation.NOT_FOUND)
            );

        public static ResultWrapper<T> FactoryCreatedOrNot(T data, bool status, List<string> messages) => new ResultWrapper<T>(
                            data,
                            status,
                            messages,
                            status ?
                                StringHelper.TranslateEnum(EReturnMessageOperation.SUCCESSFULLY_ADDED) :
                                StringHelper.TranslateEnum(EReturnMessageOperation.SOMETHING_WENT_WRONG)
            );
        public static ResultWrapper<T> FactoryCreatedOrNotWithCustomMessage(T data, bool status, List<string> messages)
        {
            var messageUi = messages[0] ??
                                (status ?
                                StringHelper.TranslateEnum(EReturnMessageOperation.SUCCESSFULLY_ADDED) :
                                StringHelper.TranslateEnum(EReturnMessageOperation.SOMETHING_WENT_WRONG));

            return new ResultWrapper<T>(
                            data,
                            status,
                            messages,
                            messageUi
            );
        }

        public static ResultWrapper<T> FactoryUpdatedOrNot(T data, bool status, List<string> messages) => new ResultWrapper<T>(
                            data,
                            status,
                            messages,
                            status ?
                                StringHelper.TranslateEnum(EReturnMessageOperation.SUCCESSFULLY_UPDATED) :
                                StringHelper.TranslateEnum(EReturnMessageOperation.SOMETHING_WENT_WRONG)
            );

        public static ResultWrapper<T> FactoryUpdateOrNotWithCustomMessage(T data, bool status, List<string> messages)
        {
            var messageUi = messages[0] ??
                                (status ?
                                StringHelper.TranslateEnum(EReturnMessageOperation.SUCCESSFULLY_UPDATED) :
                                StringHelper.TranslateEnum(EReturnMessageOperation.SOMETHING_WENT_WRONG));

            return new ResultWrapper<T>(
                            data,
                            status,
                            messages,
                            messageUi
            );
        }

        public static ResultWrapper<T> FactoryDeletedOrNot(T data, bool status, List<string> messages) => new ResultWrapper<T>(
                            data,
                            status,
                            messages,
                            status ?
                                StringHelper.TranslateEnum(EReturnMessageOperation.SUCCESSFULLY_DELETED) :
                                StringHelper.TranslateEnum(EReturnMessageOperation.SOMETHING_WENT_WRONG)
            );
    }
}
