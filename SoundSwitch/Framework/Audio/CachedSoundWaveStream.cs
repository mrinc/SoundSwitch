using System;
using NAudio.Wave;

namespace SoundSwitch.Framework.Audio
{
    public class CachedSoundWaveStream : WaveStream
    {
        private readonly CachedSound _cachedSound;

        public CachedSoundWaveStream(CachedSound cachedSound)
        {
            this._cachedSound = cachedSound;
        }

        public override WaveFormat WaveFormat => _cachedSound.WaveFormat;
        public override int Read(byte[] buffer, int offset, int count)
        {
            var availableSamples = _cachedSound.AudioData.Length - Position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(_cachedSound.AudioData, Position, buffer, offset, samplesToCopy);
            Position += samplesToCopy;
            return (int)samplesToCopy;
        }

        public override long Length => _cachedSound.AudioData.Length;
        public override long Position { get; set; }
    }
}