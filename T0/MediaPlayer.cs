using System.Collections.Generic;
using T0.Medias;

namespace T0
{
    public class MediaPlayer
    {
        //public float PlayDelay { get; set; }

        private readonly Queue<Media> _queue;

        public MediaPlayer()
        {
            _queue = new Queue<Media>();
        }

        public void PlayList(List<Media> medias)
        {
            //LINQ?????
            foreach (Media media in medias)
            {
                _queue.Enqueue(media);
            }
        }

        public void PlayAll()
        {
            foreach (Media media in _queue)
            {
                media.Play();
            }
        }

        public void Play(Media media)
        {
            Clear();
            _queue.Enqueue(media);
        }

        public void AddToQueue(Media media)
        {
            _queue.Enqueue(media);
        }

        public void Clear()
        {
            _queue.Clear();
        }
    }
}