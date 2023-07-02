using System.Collections;
using System.Collections.Generic;

public class Constants {
    
    public static readonly List<Chapter> Chapters = new List<Chapter>() {
        { new Chapter("The Grid", "Chapter0", "Chapter1") },
        { new Chapter("Fundamentals", "Chapter1", "Chapter2") },
        { new Chapter("Taking a break", "Chapter2", "Chapter3") },
        { new Chapter("Fundamentals++", "Chapter3", "Chapter4") },
        { new Chapter("Going outside", "Chapter4", "Chapter5") },
        { new Chapter("Beyond borders", "Chapter5", "Chapter6") },
        { new Chapter("Objects", "Chapter6", "Chapter7") },
        { new Chapter("The Calculator", "Chapter7", "Chapter8") },
        { new Chapter("Advanced features", "Chapter8", "Chapter9") },
        { new Chapter("Algorithms", "Chapter9", "") },
    };

    public static readonly Dictionary<string, List<Puzzle>> Puzzles = new Dictionary<string, List<Puzzle>>() {
        { "Chapter0", new List<Puzzle>() {
            new Puzzle("Your very first puzzle", "You'll be solving a simple coding puzzle to get used to the interface", "Level1"),
            new Puzzle("Your second puzzle", "Getting to know int type", "Level2"),
            new Puzzle("Last tutorial puzzle","Variable","Level3")
            }},
        {"Chapter1",new List<Puzzle>() {
            new Puzzle("First real puzzle","Fill","Level1"),
            new Puzzle("Second real puzzle","Fill","Level2"),
            new Puzzle("Third real puzzle","Fill","Level3")
        }},
        {"Chapter2",new List<Puzzle>() {
            new Puzzle("First real puzzle","Fill","Level1"),
            new Puzzle("Second real puzzle","Fill","Level2"),
            new Puzzle("Third real puzzle","Fill","Level3")
        }},
        {"Chapter3",new List<Puzzle>() {
            new Puzzle("First real puzzle","Fill","Level1"),
            new Puzzle("Second real puzzle","Fill","Level2"),
            new Puzzle("Third real puzzle","Fill","Level3")
        }}
    };
}