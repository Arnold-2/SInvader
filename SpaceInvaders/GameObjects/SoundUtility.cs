using System;

namespace SpaceInvaders
{
    public class SoundUtility
    {
        private static SoundUtility privInstance;
        IrrKlang.ISoundEngine sndEngine;
        IrrKlang.ISoundSource sndExplosion;
        IrrKlang.ISoundSource sndAdvance1;
        IrrKlang.ISoundSource sndAdvance2;
        IrrKlang.ISoundSource sndAdvance3;
        IrrKlang.ISoundSource sndAdvance4;
        IrrKlang.ISoundSource sndShoot;
        IrrKlang.ISoundSource sndAlienKilled;
        IrrKlang.ISoundSource sndUFO;
        IrrKlang.ISound UFOSound;
        int advcnt = 4;


        public SoundUtility()
        {
            sndEngine = new IrrKlang.ISoundEngine();
            sndExplosion = sndEngine.AddSoundSourceFromFile("explosion.wav");
            sndAdvance1 = sndEngine.AddSoundSourceFromFile("fastinvader1.wav");
            sndAdvance2 = sndEngine.AddSoundSourceFromFile("fastinvader2.wav");
            sndAdvance3 = sndEngine.AddSoundSourceFromFile("fastinvader3.wav");
            sndAdvance4 = sndEngine.AddSoundSourceFromFile("fastinvader4.wav");
            sndShoot = sndEngine.AddSoundSourceFromFile("shoot.wav");
            sndAlienKilled = sndEngine.AddSoundSourceFromFile("invaderkilled.wav");
            sndUFO = sndEngine.AddSoundSourceFromFile("ufo_highpitch.wav");

        }

        public static SoundUtility getInstance()
        {
            if (privInstance == null)
                privInstance = new SoundUtility();

            return privInstance;
        }

        public void playExplosion()
        {
            sndEngine.Play2D(sndExplosion, false, false, false);
        }   

        public void shoot()
        {
            sndEngine.Play2D(sndShoot, false, false, false);
        }

        public void killed()
        {
            sndEngine.Play2D(sndAlienKilled, false, false, false);
        }

        public IrrKlang.ISound StartUFO()
        {
            UFOSound = sndEngine.Play2D(sndUFO, true, false, true);
            return UFOSound;
        }

        public void Advancing()
        {
            if (this.advcnt >= 4)
                advcnt = 1;
            else
                advcnt++;

            if (this.advcnt == 1)
                sndEngine.Play2D(sndAdvance1, false, false, false);
            else if (this.advcnt == 2)
                sndEngine.Play2D(sndAdvance2, false, false, false);
            else if (this.advcnt == 3)
                sndEngine.Play2D(sndAdvance3, false, false, false);
            else if (this.advcnt == 4)
                sndEngine.Play2D(sndAdvance4, false, false, false);
        }

        public void Update()
        {
            this.sndEngine.Update();
        }
    }
}
