using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shizui
{
    class Query
    {
        // Clase encargada de obtener el dato buscado eficientemente en las consultas en datos (skills, items, etc)
        List<string> skills;
        Dictionary<string, int> skillsQuery = new Dictionary<string, int>();
        Dictionary<string, int> modifiedQuery = new Dictionary<string, int>();

        // Esta variable contendra el dato de la query actual, es decir, si estoy buscando "aqua sw"
        // el valor de query sera "aqua sw".
        string previousQuery = "";
        string currentQuery = "";

        public Query(List<string> skills)
        {
            this.skills = skills;
            this.skills.Sort();

            foreach (string skill in skills)
            {
                // inicializo todos los skills con lvl 0
                if (!skillsQuery.ContainsKey(skill))
                {
                    skillsQuery.Add(skill, 0);
                }
            }
        }

        void Init()
        {
            foreach (KeyValuePair<string, int> skill in skillsQuery.ToList())
            {
                skillsQuery[skill.Key] = 0;
            }
            Debug.WriteLine("OWATA!");
        }

        public List<string> PerformQuery(string query, int maxResults = 10)
        {
            if (maxResults > skills.Count) maxResults = skills.Count;
            currentQuery = query;
            List<string> resultQuery;

            int indexOfCoincidence = GetIndexOfCoincidence(previousQuery, currentQuery);
            int difference = currentQuery.Length - previousQuery.Length;

            foreach (KeyValuePair<string, int> currentData in skillsQuery.ToList())
            {
                int skillLevel = currentData.Value;

                // verifico si la diferencia en tamanio del query anterior al actual es positiva o negativa
                if (difference > 0)
                {
                    if (skillLevel < indexOfCoincidence) continue;

                    for (int i = indexOfCoincidence; i < indexOfCoincidence + difference; i++)
                    {
                        // incremento niveles si coincide.
                        IncreaseLevelIfNecessary(currentData, i);

                        // pongo en true a levelWasIncrease solamente si al menos una vez el resultado fue true.
                    }

                }
                else if (difference < 0)
                {
                    // en este caso significa que borre un caracter o mas del query anterior.

                    // si el nivel del skill actual es <= al indice de coincidencia, entonces no me interesa.
                    if (skillLevel <= indexOfCoincidence) continue;

                    for (int i = indexOfCoincidence; i < indexOfCoincidence + Math.Abs(difference); i++)
                    {
                        // bajo de nivel a todos los que son nivel superior al indice de coincidencia
                        DecreaseLevelIFNecessary(currentData, i);
                        skillLevel = skillsQuery[currentData.Key];
                        if (skillLevel <= indexOfCoincidence) break;
                    }
                    // subo de nivel en caso de que sea necesario, es decir, si hay un match de awa pero no de awada, borro luego
                    // la d quedando awaa, debe verificarse en el skill de nivel, en este caso 3, si tiene match de la ultima a
                    int fixedIndex = indexOfCoincidence + Math.Abs(indexOfCoincidence - skillLevel);
                    for (; fixedIndex < currentQuery.Length; fixedIndex++)
                    { 
                        IncreaseLevelIfNecessary(currentData, fixedIndex);
                    }

                }
                else
                {
                    Debug.WriteLine("DIFERENCIA = 0... wtf :o");
                }
            }
            
            // tomo los primeros resultados de la query y lo agrego a la lista.
            resultQuery = GetBestMatch(maxResults);

            previousQuery = currentQuery;
            return resultQuery;
        }

        List<string> GetBestMatch(int maxResults)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<Dictionary<string, int>> ordererListSkill = new List<Dictionary<string, int>>();
            List<string> bestMatch = new List<string>();

            // agrego a la lista una cantidad de diccionarios igual a el lvl maximo posible en esta query
            for (int i = 0; i <= currentQuery.Length; i++)
            {
                ordererListSkill.Add(new Dictionary<string, int>());
            }

            // separo la lista por niveles.
            foreach (KeyValuePair<string, int> skill in skillsQuery)
            {
                int index = currentQuery.Length - skill.Value;

                try
                {
                    ordererListSkill[index].Add(skill.Key, skill.Value);
                } catch(IndexOutOfRangeException e)
                {
                    MessageBox.Show("Error al ordenar la lista de query, Error msj: " + e.Message + ". StackTrace: " + e.StackTrace);
                    return bestMatch;
                }
            }

            bestMatch.Add("");
            foreach (Dictionary<string, int> dictSkills in ordererListSkill)
            {
                foreach (KeyValuePair<string, int> skill in dictSkills)
                {
                    bestMatch.Add(skill.Key);
                    maxResults--;
                    if (maxResults == 0) break;
                }
                if (maxResults == 0) break;
            }

            watch.Stop();
            Console.WriteLine("Tiempo de ejecucion de la funcion GetBestMatch " + watch.ElapsedMilliseconds.ToString());

            return bestMatch;
        }

        public bool DecreaseLevelIFNecessary(KeyValuePair<string, int> currentData, int index)
        {

            foreach (char chr in currentData.Key)
            {
                if (char.ToLower(chr) == char.ToLower(previousQuery[index]))
                {
                    skillsQuery[currentData.Key] --;
                    return true;

                }
            }
            return false;
        }

        public bool IncreaseLevelIfNecessary(KeyValuePair<string, int> currentData, int index)
        {
            if (index >= currentQuery.Length) return false;
            int skillLevel = skillsQuery[currentData.Key];
            string alreadyLearned = "";
            if (previousQuery != "")
            {
                alreadyLearned = previousQuery.Substring(0, skillLevel);
            }

            int alreadyLearnedCharCount = 0;
            if (alreadyLearned != "")
            {
                foreach (char alrChr in alreadyLearned)
                {
                    if (char.ToLower(alrChr) == char.ToLower(currentQuery[index]))
                    {
                        alreadyLearnedCharCount++;
                    }
                }
            }

            foreach (char chr in currentData.Key)
            {
                if (char.ToLower(chr) == char.ToLower(currentQuery[index]))
                {
                    // Coincide, antes de subir de nivel, verifico si ya previamente habia subido de nivel el skill
                    // con este mismo caracter, si es asi, tengo que comprobar que haya otro caracter mas para evitar
                    // subir de nivel con un mismo caracter en un skill que solo posea uno solo de esos caracteres.
                    // Entonces, por cada character repetido ya aprendido aumento en 1 el contador.
                    // Si hay un match y el contador esta en 0, significa que tengo que aprender el skill
                    // Si hay un match y el contador es > 0, significa que ya subi de nivel con ese char antes
                    // y debo entonces restar uno al contador.


                    // si es cero el contador, significa que es el turno de subir de nivel, yeey!
                    if (alreadyLearnedCharCount == 0)
                    {
                        skillsQuery[currentData.Key] ++;
                        return true;
                    } else
                    {
                        alreadyLearnedCharCount--;
                    }
                }
            }
            return false;
        }

        public int GetIndexOfCoincidence(string previousQuery, string currentQuery)
        {
            // Devuelve el indice del string en donde la query previa difiere con la query actual
            int index = 0;

            for (int i = 0; i < currentQuery.Length; i++)
            {
                try
                {
                    if (previousQuery[i] == currentQuery[i]) index++; else break;
                } catch(IndexOutOfRangeException e)
                {
                    // llegaria para cuando o previousQuery o currentQuery no tiene ningun dato asignado
                    // eso sucede cuando recien comienza la query o cuando se borra toda la query.
                    break;
                }
            }

            return index;
        }

        public void Reset()
        {
            // reseteo query
            currentQuery = "";
            previousQuery = "";
            skills.Clear();
            Init();
        }
    }
}
