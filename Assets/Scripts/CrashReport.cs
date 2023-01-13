/* 
 * Programer: Jack
 * Purpose: To Make the CrashReport.txt file the error history
*/
using UnityEngine;

public class CrashReport : MonoBehaviour
{
    private void Awake()
    {
        System.IO.File.Delete("CrashReport");
        System.IO.File.Create("CrashReport");
    }
    void Update()
    {
        System.IO.File.WriteAllText("CrashReport", string.Empty);
        System.IO.File.WriteAllTextAsync("CrashReport", UnityEngine.CrashReport.reports.ToString());
    }
}
