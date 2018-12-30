using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

public class PermTest {
    public static void Main() {
        int[] a = {1, 2, 3, 4, 5};
        var perms = Permutation<int>.Permutate(a, 4, true);
        WriteLine("num of perms: {0}", perms.Count());
        foreach (var i in perms) {
            WriteLine(String.Join(", ", i));
        }
    }
}

static class Permutation<T> {
    private static int selectNum;
    private static bool duplicate;
    
    private static void Select(List<T[]> perms, Stack<T> stack, List<T> list) {
        if (stack.Count == selectNum) {
            perms.Add(stack.Reverse().ToArray());
        }
        else {
            var subList = new List<T>(list);
            if (!duplicate) {
                subList.RemoveAt(0);
            }
            for (var i = 0; i < list.Count(); i++) {
                stack.Push(list[i]);
                Select(perms, stack, subList);
                if (i < subList.Count()) {
                    subList[i] = list[i];
                }
                stack.Pop();
            }
        }
    }
    
    public static IEnumerable<T[]> Permutate(IEnumerable<T> src, int n = 0, bool dupl = false) {
        var perms = new List<T[]>();
        var srcList = new List<T>(src);
        
        selectNum = n > 0 ? n : srcList.Count();
        duplicate = dupl;
        Select(perms, new Stack<T>(), srcList);
        return perms;
    }
}