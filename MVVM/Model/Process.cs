using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EasySaveApp.MVVM.Model
{
    public class ProcessModel
    {
        // Récupère tous les processus en cours d'exécution sur l'ordinateur
        public List<Process> GetProcesses()
        {
            // Créez une nouvelle liste vide de processus
            List<Process> processes = new List<Process>();

            // Récupérez tous les processus en cours d'exécution sur l'ordinateur
            Process[] allProcesses = Process.GetProcesses();

            // Parcourez tous les processus et ajoutez-les à la liste
            foreach (Process p in allProcesses)
            {
                processes.Add(p);
            }
            return processes;
        }
    }
}