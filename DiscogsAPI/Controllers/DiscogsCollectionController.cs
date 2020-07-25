using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DiscogsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscogsCollectionController : ControllerBase
    {

        //Retourne par défaut 5 disques (Release) aléatoires parmis la collection Discogs importer dans Program.cs.
        //On peut utiliser le paramètre nbRelease pour retourner le nombre de disque désiré, les valeurs possibles sont de 0 à 5.
        //Exemple d'un lien utilisant le paramètre: https://localhost:5001/discogscollection?nbRelease=3.
        [HttpGet]
        public IEnumerable<Release> Get(int nbRelease = 5)
        {
            var rng = new Random();
            int releasesCount = Program.discogsCollection.releases.Count();

            //Pour éviter les erreurs d'index et limiter le nombre de résultat pour la démonstration.
            //On retourne 5 disques si la valeur du paramètre est négative ou supérieur à 5.
            if (nbRelease < 0 || nbRelease > 5) { nbRelease = 5; }

            return Enumerable.Range(0, nbRelease).Select(index => Program.discogsCollection.releases.ElementAt(rng.Next(0, releasesCount - 1)));
        }
    }
}

