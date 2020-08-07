namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// 表示无法根据ID找到组件的异常
    /// </summary>
    public class IdNotFoundException : ComponentNotFoundException
    {
        /// <summary>
        /// 构建无法根据ID找到组件的异常
        /// </summary>
        /// <param name="id">组件的id</param>
        public IdNotFoundException(string id) :
            base($"Id '{id}' has not been found in lake")
        {
        }
    }
}
