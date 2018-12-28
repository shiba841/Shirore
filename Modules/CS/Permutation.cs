using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

public class PermTest {
    public static void Main() {
        int[] a = {1, 2, 3, 4};
        foreach (var i in Permutation<int>.Permutate(a, 3)) {
            WriteLine(String.Join(", ", i));
        }
    }
}

static class Permutation<T> {
    private static int selectNum;
    
    private static void Select(List<T[]> perms, Stack<T> stack, List<T> list) {
        if (stack.Count == selectNum) {
            perms.Add(stack.Reverse().ToArray());
        }
        else {
            var subList = new List<T>(list);
            subList.RemoveAt(0);
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
    
    public static IEnumerable<T[]> Permutate(IEnumerable<T> src, int n = 0) {
        var perms = new List<T[]>();
        var srcList = new List<T>(src);
        
        selectNum = n > 0 ? n : srcList.Count();
        Select(perms, new Stack<T>(), srcList);
        return perms;
    }
}