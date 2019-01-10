using System;
using System.Collections.Generic;
using static System.Console;

public class RootSearch {
    
    public static void Main () {
        var line = ReadLine().Trim().Split(' ');
        var M = Int32.Parse(line[0]);
        var N = Int32.Parse(line[1]);
        // 迷路を作成
        var map = new string[N][];
        for (var n = 0; n < N; n++) {
            map[n] = ReadLine().Trim().Split(' ');
        }
        
        // マップ情報を渡して探索
        var maze = new Maze(map, N, M);
        var distance = maze.Search();
        // 距離が-1 (ゴール未到達) の場合
        if (distance < 0) {
            WriteLine("Fail");
        }
        // 到達できた場合
        else {
            WriteLine(distance);
        }
    }
    
}

// 迷路探索用クラス
public class Maze {
    
    private string[][] map;     // 迷路のマップ情報
    private int height;         // 迷路の高さ
    private int width;          // 迷路の幅
    private int[][] visited;    // スタートからの距離 (未到達時-1)
    private Position start;     // スタート位置
    private Position goal;      // ゴール位置

    // x座標，y座標をまとめて持つ構造体
    private struct Position {
        public int x { get; set; }
        public int y { get; set; }
        public Position (int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
    
    // コンストラクタ
    public Maze (string[][] map, int height, int width) {
        this.map = map;
        this.height = height;
        this.width = width;
        // 配列を生成，要素は全て-1
        this.visited = new int[height][];
        for (var h = 0; h < height; h++) {
            visited[h] = new int[width];
            for (var w = 0; w < width; w++) {
                visited[h][w] = -1;
            }
        }
        
        // 各々セットしたらtrue
        var setStart = false;
        var setGoal = false;
        // スタート位置，ゴール位置をセット
        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {
                if (map[y][x].Equals("s")) {
                    start = new Position(x, y);
                    setStart = true;
                }
                if (map[y][x].Equals("g")) {
                    goal = new Position(x, y);
                    setGoal = true;
                }
            }
            // 両方セットしたら脱ループ
            if (setStart && setGoal){
                break;
            }
        }
    }
    
    // 探索
    public int Search () {
        // 探索位置の待ち行列
        var searchQue = new Queue<Position>();
        // 探索方向
        var directions = 4;
        
        searchQue.Enqueue(start);
        visited[start.y][start.x] = 0;
        // 探索位置がなくなるまでループ
        while (searchQue.Count > 0) {
            var currentPos = searchQue.Dequeue();
            
            // 全探索方向を順に調べる
            for (var dir = 0; dir < directions; dir++) {
                var nextPos = new Position(currentPos.x, currentPos.y);
                switch (dir) {
                    case 0: // 上
                        nextPos.y -= 1;
                        break;
                    case 1: // 右
                        nextPos.x += 1;
                        break;
                    case 2: // 下
                        nextPos.y += 1;
                        break;
                    case 3: // 左
                        nextPos.x -= 1;
                        break;
                }
                
                // 移動が必要の場合
                if (NeedSearch(nextPos)) {
                    // スタートからの距離を記録して次位置を待ち行列へ
                    visited[nextPos.y][nextPos.x] = visited[currentPos.y][currentPos.x] + 1;
                    searchQue.Enqueue(nextPos);
                    
                    // 次位置がゴールの場合
                    if (PositionMatching(nextPos, goal)) {
                        // 待ち行列をクリアして脱ループ
                        searchQue.Clear();
                        break;
                    }
                }
            }
        }
        // ゴールまでの距離を返す
        return visited[goal.y][goal.x];
    }
    
    // 移動判定
    private bool NeedSearch (Position pos) {
        var x = pos.x;
        var y = pos.y;
        
        // 迷路の範囲内
        if (0 <= x && x < width && 0 <= y && y < height) {
            // 未到達かつ壁でない場合
            if (visited[y][x] == -1 && !map[y][x].Equals("1")) {
                return true;
            }
        }
        return false;
    }
    
    // 2つの位置のマッチ判定
    private bool PositionMatching (Position pos, Position match) {
        if (pos.x == match.x && pos.y == match.y) {
            return true;
        }
        else {
            return false;
        }
    }
    
}