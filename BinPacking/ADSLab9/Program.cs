Console.WriteLine("Bin Packing");

List<Double> data = ReadWriteFile.readData("C:\\Users\\tomgr\\OneDrive - Sheffield Hallam University\\Documents\\university\\Coursework documents\\ADS\\BinPacking\\ADSLab9\\dataset.csv");
HillClimbing hc = new HillClimbing(data);

List<Double> results = new List<Double>(10);

for (int i = 0; i < 20; i++)
{
    Console.WriteLine("\n\nITERATION ", i, "\n");
    //hc.runHC(10);

    double finalFitness = hc.runHC(20);
    results.Add(finalFitness);
}

Console.WriteLine("\n\nResults: ");
foreach (Double result in results)
{
    Console.WriteLine(result);
}
Console.WriteLine("\n\nFitnesses, run 1: ");
for (int i = 0; i < 15; i++)
{
    Console.WriteLine(hc.fitnessResults[i]);
}

int choice = Convert.ToInt32(Console.ReadLine());
// Ensure the input choice is valid
if (choice >= 0 && choice < hc.solutions.Count)
{
    // Retrieve the bins of the selected solution
    List<List<double>> selectedSolution = hc.solutions[choice];

    // Display the bins of the selected solution
    Console.WriteLine("Bins for iteration " + choice + ":");
    for (int binIndex = 0; binIndex < selectedSolution.Count; binIndex++)
    {
        Console.Write("Bin " + (binIndex + 1) + ": ");
        foreach (double item in selectedSolution[binIndex])
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}




// List<Double> data = ReadWriteFile.readData("data.csv");
// HillClimbing hc = new HillClimbing(data);
// hc.runSHC(10);