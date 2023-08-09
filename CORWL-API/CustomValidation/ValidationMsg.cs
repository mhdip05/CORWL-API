using CORWL_API.Extension;

namespace CORWL_API.CustomValidation
{
#nullable disable
    public static class ValidationMsg
    {
        public static bool SameObjectDetection(object source1, object source2)
        {
            if (source1.Equals(source2)) return true;

            return false;
        }

        public static string SameObjectDetectionMessage()
        {
            return $"It seems that you did not update anything for";
        }

        public static string NoRecordFound()
        {
            return "No Record Found";
        }

        public static string SomethingWrong(string state = null)
        {
            if (state == null) return "Something went wrong";

            return $"Something went wrong while {state}";
        }

        public static string Exist(string entity = null)
        {
            if (entity == null) return "Data Already Exists";

            return $"{entity.ToCapitalize()} Already Exists";
        }


    }
}
