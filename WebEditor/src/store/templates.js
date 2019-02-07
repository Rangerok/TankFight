export const jsTemplate = `const linq = require('linq'),
    readline = require('readline'),
    log = console.log,
    rl = readline.createInterface(process.stdin, process.stdout),
    botName = 'vblz/tanks:jsrubot';

const UserActionType = { Move: 0, Shoot: 1},
    Direction = {Left: 0, Up: 1, Right: 2, Down: 3},
    CellType = {Tank: 0, Barrier: 1, NotDestroyable: 2, Water: 3};

rl.on('line', (state) => {
    let userAction = [];
    let gameState = JSON.parse(state);
    let myTank = linq.from(gameState.ContentsInfo).firstOrDefault((c) => c.Type === CellType.Tank && c.UserId === botName).Coordinates;
    let enemyTank = linq.from(gameState.ContentsInfo).firstOrDefault((c) => c.Type === CellType.Tank && c.UserId !== botName).Coordinates;
    let dX = myTank.X - enemyTank.X;
    let dY = myTank.Y - enemyTank.Y;

    let direction = (Math.random() > 0.7) ? (Math.floor(Math.random() * 4)) : (Math.abs(dX) > Math.abs(dY)
        ? (dX < 0 ? Direction.Right : Direction.Left)
        : (dY < 0 ? Direction.Up : Direction.Down));

    let action = (Math.random() > 0.5) ? (UserActionType.Move) : (UserActionType.Shoot);

    userAction.push({Type: action, Direction: direction});
    log(userAction);
});`;

export const goTemplate = `package main

import (
	"bufio"
	"encoding/json"
	"math/rand"
	"os"
)

type Coordinates struct {
	X int
	Y int
}

type CellContentType byte

const (
	Tank           CellContentType = 0
	Barrier        CellContentType = 1
	NotDestroyable CellContentType = 2
	Water          CellContentType = 3
	Spawn          CellContentType = 4
)

type CellContentInfo struct {
	Coordinates Coordinates
	HealthCount byte
	Type        CellContentType
	UserId      string
}

type Direction byte

const (
	Up    Direction = 0
	Down  Direction = 1
	Left  Direction = 2
	Right Direction = 3
)

type BulletInfo struct {
	Coordinates Coordinates
	Direction   Direction
	OwnerId     string
}

type GameState struct {
	ContentsInfo []CellContentInfo
	BulletsInfo  []BulletInfo
	ZoneRadius   byte
}

type UserActionType byte

const (
	Move  UserActionType = 0
	Shoot UserActionType = 1
)

type UserAction struct {
	Type      UserActionType
	Direction Direction
}

func main() {
	stdin := bufio.NewScanner(os.Stdin)
	enc := json.NewEncoder(os.Stdout)
	gameState := GameState{}
	for stdin.Scan() {
		json.Unmarshal([]byte(stdin.Text()), &gameState)
		userAction := UserAction{Type: UserActionType(rand.Intn(2)), Direction: Direction(rand.Intn(4))}
		enc.Encode([]UserAction{userAction})
	}
}`;

export const csharpTemplate = `using System;
using System.Threading;
using Newtonsoft.Json;

namespace RandomBot
{
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
    }

	public enum UserActionType
	{
		Move,
		Shoot
    }

	public class UserAction
	{
		public UserActionType Type { get; set; }
		public Direction Direction { get; set;  }
    }

    class Program
    {
        private const int MovePercent = 70;
        private static readonly Random random = new Random();

        private static readonly CancellationTokenSource cts = new CancellationTokenSource();
        
        private static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eventArgs) => cts.Cancel();
            var ct = cts.Token;
            int moveNumber = 0;
            
            while (!ct.IsCancellationRequested)
            {
                var state = Console.ReadLine();
                
                Console.Error.WriteLine("Move number {0}", moveNumber++);
                // следующий тред слип нужен, чтобы stderr долетел раньше stdout
                Thread.Sleep(10);
                
                Console.WriteLine(JsonConvert.SerializeObject(new UserAction[]
                {
                    new UserAction()
                    {
                        Direction = (Direction)random.Next(4),
                        Type = random.Next(100) > MovePercent ? UserActionType.Shoot : UserActionType.Move
                    }
                }));
            }
        }
    }
}`;

export const pythonTemplate = `#!/usr/bin/env python3
import random
import json

botName = 'vblz/tanks:pyrubot'

while True:
    state = input()
    gameState = json.loads(state)
    myTank = next(c for c in gameState["ContentsInfo"] if c["Type"] == 0 and c["UserId"] == botName)["Coordinates"]
    enemyTank = next(c for c in gameState["ContentsInfo"] if c["Type"] == 0 and c["UserId"] != botName)["Coordinates"]
    dX = myTank["X"] - enemyTank["X"]
    dY = myTank["Y"] - enemyTank["Y"]
    if random.randint(0, 100) < 70:
        direction = ((0 if dX < 0 else 2) if (abs(dX) > abs(dY)) else (1 if dY < 0 else 3))
    else:
        direction = (random.randint(0, 3))

    print([{'Type': 0 if random.randint(0, 100) < 70 else 1, 'Direction': direction}])`;
