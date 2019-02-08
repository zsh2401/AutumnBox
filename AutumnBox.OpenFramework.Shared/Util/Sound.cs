using AutumnBox.OpenFramework.Management;

namespace AutumnBox.OpenFramework.Util
{
    /// <summary>
    /// 秋之盒音效API
    /// </summary>
    public static class Sound
    {
        /// <summary>
        /// 声音ID
        /// </summary>
        public enum Id
        {
            /// <summary>
            /// OK
            /// </summary>
            Ok
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="id"></param>
        public static void Play(Id id)
        {
            switch (id)
            {
                case Id.Ok:
                    OpenFx.BaseApi.PlayOk();
                    break;
            }
        }
    }
}
