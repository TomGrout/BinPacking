public class BinPacking : HillClimbing
{
    public List<List<double>> bins = new List<List<double>>(); // Represents bins and their contents
    public double fitness = 0.00;
    public static double binCapacity = 100.0; // Capacity of each bin

    public BinPacking() : base(data)
    {
        initializeBins();
        calculateFitness();
    }

    // Initialize bins by assigning each item to a new bin
    private void initializeBins()
    {
        foreach (var item in data)
        {
            bins.Add(new List<double> { item });
        }
    }

    // Calculate fitness as 1 / (number of bins + 1)
    public void calculateFitness()
    {
        fitness = 10.0 / (bins.Count + 1);
    }

    // Make a random small change: move one item from a random bin to another (if space allows)
    public BinPacking smallChange()
    {
        Random rand = new Random();

        // Deep copy bins for modification
        List<List<double>> newBins = bins.Select(bin => new List<double>(bin)).ToList();

        // Pick a random source bin
        int sourceBinIndex = rand.Next(newBins.Count);
        while (newBins[sourceBinIndex].Count == 0) // Ensure non-empty bin
        {
            sourceBinIndex = rand.Next(newBins.Count);
        }

        // Pick a random item within the source bin
        int itemIndex = rand.Next(newBins[sourceBinIndex].Count);
        double item = newBins[sourceBinIndex][itemIndex];

        // Remove the item from the source bin
        newBins[sourceBinIndex].RemoveAt(itemIndex);

        // If the source bin is now empty, remove it
        if (newBins[sourceBinIndex].Count == 0)
        {
            newBins.RemoveAt(sourceBinIndex);
        }

        // Try to place the item in an existing bin
        bool placed = false;
        foreach (var bin in newBins)
        {
            if (bin.Sum() + item < 130) // Check if the item fits in the bin
            {
                bin.Add(item);
                placed = true;
                break;
            }
        }

        // If no existing bin can take the item, create a new bin
        if (!placed)
        {
            Console.WriteLine($"Creating a new bin for item {item}.");
            newBins.Add(new List<double> { item });
        }

        // Create a new solution with updated bins
        BinPacking newSol = new BinPacking();
        newSol.copyBins(newBins);
        return newSol;
    }




    // Copy bins from another solution
    public void copyBins(List<List<double>> otherBins)
    {
        bins = otherBins.Select(bin => new List<double>(bin)).ToList();
        calculateFitness();
    }

    // Print the solution (bins and their contents)
    public void printBins()
    {
        Console.WriteLine("Bins:");
        for (int i = 0; i < bins.Count; i++)
        {
            Console.Write($"Bin {i + 1}: ");
            Console.WriteLine(string.Join(", ", bins[i]));
        }
        Console.WriteLine($"Fitness: {fitness}");
    }
    public void genRandomBins()
    {
        bins = new List<List<double>>();
        Random r = new Random();

        foreach (double item in HillClimbing.data)
        {
            bool placed = false;

            // Try to place the item in an existing bin
            foreach (var bin in bins)
            {
                if (bin.Sum() + item <= binCapacity)
                {
                    bin.Add(item);
                    placed = true;
                    break;
                }
            }

            // If no bin can accommodate the item, create a new bin
            if (!placed)
            {
                bins.Add(new List<double> { item });
            }
        }

        calculateFitness();
    }

}
