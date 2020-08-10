/*

* ==============================================================================
*
* Filename: SoundServiceImpl
* Description: 
*
* Version: 1.0
* Created: 2020/8/11 5:14:46
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using System.IO;
using System.Media;
using System.Reflection;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ISoundService))]
    class SoundServiceImpl : ISoundService
    {
        readonly Assembly currentAssembly;
        readonly SoundPlayer? okSoundPlayer;
        readonly ISettings settings;
        public SoundServiceImpl(ISettings settings)
        {
            this.settings = settings;
            currentAssembly = GetType().Assembly;
            okSoundPlayer = LoadSoundPlayer("ok.wav");
        }

        private SoundPlayer? LoadSoundPlayer(string soundName)
        {
            string pathToSoundResource = currentAssembly.GetName().Name + ".Resources.Sounds." + soundName;
            Stream? soundStream = currentAssembly.GetManifestResourceStream(pathToSoundResource);
            if (soundStream == null) return null;
            return new SoundPlayer(soundStream);
        }
        public void PlayOK()
        {
            if (settings.SoundEffect && okSoundPlayer != null)
            {
                okSoundPlayer.Play();
            }
        }
    }
}
