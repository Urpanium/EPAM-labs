using T0.Medias;

namespace T0
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Sound sound1 = new Sound("Sound 1", 1.5f, 150);
            Sound sound2 = new Sound("Sound 2", 2.5f, 250);
            Sound sound3 = new Sound("Sound 3", 3.5f, 350);
            Sound sound4 = new Sound("Sound 4", 4.5f, 450);

            Resolution r1 = new Resolution(1, 1);
            Resolution r2 = new Resolution(2, 2);
            Resolution r3 = new Resolution(3, 3);
            Resolution r4 = new Resolution(4, 4);
            
            Video video1 = new Video("Video 1", r1, 150);
            Video video2 = new Video("Video 2", r2, 250);
            Video video3 = new Video("Video 3", r3, 350);
            Video video4 = new Video("Video 4", r4, 450);
            
            Image image1 = new Image("Image 1", r1, 1);
            Image image2 = new Image("Image 2", r2, 2);
            Image image3 = new Image("Image 3", r3, 3);
            Image image4 = new Image("Image 4", r4, 4);
            
            MediaPlayer mediaPlayer = new MediaPlayer();
            
            mediaPlayer.AddToQueue(sound1);
            mediaPlayer.AddToQueue(sound2);
            mediaPlayer.AddToQueue(sound3);
            mediaPlayer.AddToQueue(sound4);
            
            mediaPlayer.AddToQueue(video1);
            mediaPlayer.AddToQueue(video2);
            mediaPlayer.AddToQueue(video3);
            mediaPlayer.AddToQueue(video4);
            
            mediaPlayer.AddToQueue(image1);
            mediaPlayer.AddToQueue(image2);
            mediaPlayer.AddToQueue(image3);
            mediaPlayer.AddToQueue(image4);
            
            mediaPlayer.PlayAll();
        }
    }
}