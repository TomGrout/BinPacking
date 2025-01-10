public class HillClimbing
{
    public static List<Double> data;
    public List<List<List<double>>> solutions = new List<List<List<double>>>();              // Store bin configurations for each iteration
    public List<double> fitnessResults = new List<double>();

    public HillClimbing(List<Double> d)
    {
        data = new List<double>(d);
    }

    public double runHC(int iter)
    {
        double[,] result = new double[iter, 3];

        // Initialize solutions
        BinPacking solution = new BinPacking(); // Initial solution
        BinPacking newSol = new BinPacking();   // Candidate solution for hill climbing
        newSol.copyBins(solution.bins);

        Console.WriteLine("Starting Hill Climbing...");
        Console.WriteLine($"Initial Fitness: {solution.fitness}");
        solution.printBins();
        Console.WriteLine();

        for (int i = 0; i < iter; i++)
        {
            // Log the current iteration and fitness
            result[i, 0] = i + 1;  // Iteration number
            result[i, 1] = solution.fitness; // Current solution fitness
            result[i, 2] = newSol.fitness;   // Candidate solution fitness

            // Store the current solution for debugging/analysis
            solutions.Add(solution.bins.Select(bin => new List<double>(bin)).ToList());

            // Generate a new solution with a small change
            newSol = solution.smallChange();
            Console.WriteLine($"Iter {i + 1}");
            Console.WriteLine($"Current Solution Fitness: {solution.fitness}");
            solution.printBins();

            Console.WriteLine($"New Solution Fitness: {newSol.fitness}");
            newSol.printBins();

            // If the new solution is better, update the current solution
            if (newSol.fitness > solution.fitness)
            {
                Console.WriteLine("New solution accepted.");
                solution.copyBins(newSol.bins);
                fitnessResults.Add(newSol.fitness);
            }
            else
            {
                Console.WriteLine("New solution rejected.");
            }
            solutions.Add(new List<List<double>>(solution.bins));

            Console.WriteLine();
        }

        // Log the final solution and fitness
        Console.WriteLine("Final solution:");
        Console.WriteLine($"Final Fitness: {solution.fitness}");
        solution.printBins();

        // Save results and solutions to files
        ReadWriteFile.writeResults(result, "result.csv");
        ReadWriteFile.writeSolutions(solutions, "solutions.csv");

        // Return the fitness of the best solution
        return solution.fitness;
    }
    


    // This method is an additional exercise for Stochastic Hill Climbing, which is not part of the tutorial.
    public void runSHC(int iter)
    {
        double T = 25.0;
        double pr = 0.00;
        double treshold = 0.46;
        // Create a new solution
        BinPacking sol = new BinPacking();
        BinPacking newSol = new BinPacking();

        // copy the existion solution to the new solution

        //newSol.copyBins(sol.solution);

        for (int i = 0; i < iter; i++)
        {
            Console.WriteLine("Iter :" + i);
            Console.WriteLine("Current fitness :" + sol.fitness);

            // perform a small change toward the new solution

            //newSol = solution.smallChange();
            Console.WriteLine("New fitness :" + newSol.fitness);
            double diff_fitness = newSol.fitness - sol.fitness;
            pr = 1 / (1 + (Math.Pow(Math.Exp(1), diff_fitness / T)));
            Console.WriteLine("Pr :" + pr + " versus the threshold " + treshold);

            if (pr > treshold)
            {
                //sol.copyBins(newSol.solution);
            }
            Console.WriteLine();
        }
        Console.WriteLine("Final fitness :" + sol.fitness);
    }

}