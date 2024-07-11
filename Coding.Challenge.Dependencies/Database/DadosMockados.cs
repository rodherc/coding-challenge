using Coding.Challenge.Dependencies.Models;


namespace Coding.Challenge.Dependencies.Database
{
    public class DadosMockados : IDadosMockados<Content>
    {
        public IDictionary<Guid, Content> GerarMocks()
        {
            IDictionary<Guid, Content> mockContents = new Dictionary<Guid, Content>();
            for (int i = 0; i < 10; i++)
            {
                Guid id = Guid.NewGuid();
                string title = GetTituloFilme(i + 1);
                string subTitle = "Subtitle for " + title;
                string description = "Description for " + title;
                string imageUrl = "https://example.com/" + title.Replace(' ', '_') + ".jpg";
                int duration = 120 + i;
                DateTime startTime = DateTime.Now.AddDays(i);
                DateTime endTime = DateTime.Now.AddDays(i).AddHours(2);
                List<string> genres = GetGenerosAleatorios();

                mockContents.Add(id, new Content(id, title, subTitle, description, imageUrl, duration, startTime, endTime, genres));
            }

            return mockContents;
        }

        private static string GetTituloFilme(int index)
        {
            // Sample movie titles
            string[] titles = {
            "A Origem",
            "Interstellar",
            "Batman",
            "Pulp Fiction",
            "Tropa de Elite",
            "Forrest Gump",
            "O Poderoso Chefão",
            "Clube da Luta",
            "Gladiador",
            "Avatar"
        };

            return titles[index % titles.Length];
        }

        private static List<string> GetGenerosAleatorios()
        {
            string[] generos = { "Ação", "Aventura", "Comédia", "Drama", "Fantasia", "Terror", "Mistério", "Romance", "Ficção Científica", "Suspense" };
            List<string> randomGenres = new List<string>();

            Random rand = new Random();
            int numGenres = rand.Next(1, 4); // Randomly choose 1 to 3 genres

            for (int i = 0; i < numGenres; i++)
            {
                int index = rand.Next(0, generos.Length);
                randomGenres.Add(generos[index]);
            }

            return randomGenres;
        }
    }
}
