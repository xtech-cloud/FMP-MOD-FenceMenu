
namespace XTC.FMP.MOD.FenceMenu.LIB.Unity
{
    public class MySubject : MySubjectBase
    {
        /// <summary>
        /// 打开内容框
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// model.Publish(/XTC/FenceMenu/OpenContent, data);
        /// </example>
        public const string OpenContent = "/XTC/FenceMenu/OpenContent";

        /// <summary>
        /// 关闭内容框
        /// </summary>
        /// <example>
        /// var data = new Dictionary<string, object>();
        /// data["uid"] = "default";
        /// model.Publish(/XTC/FenceMenu/CloseContent, data);
        /// </example>
        public const string CloseContent = "/XTC/FenceMenu/CloseContent";
    }
}
