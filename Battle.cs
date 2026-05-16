class Battle
{

    public string StartBattle(Player player, Enemy enemy)
    {
        while (player.IsAlive && enemy.IsAlive)
        {
            Console.Clear();
            UI.BattleStats(player, enemy);

            Console.WriteLine(" [A] for Attack");
            Console.WriteLine(" [R] for Run");
            Console.WriteLine("=====================");    // <--- ändra till UI.ShowBattleStatsAndOptions typ
            string battleChoice = Console.ReadLine().ToUpper();

            if (battleChoice == "A")
            {
                int damageToEnemy = player.Attack;
                enemy.HP -= damageToEnemy;

                Console.WriteLine($"You did {damageToEnemy} damage.");
                if (enemy.HP <= 0)
                {
                    Console.WriteLine("You are victorious!");
                    Console.ReadKey();
                    return "WIN";
                }

                Console.ReadKey();

                int damageToPlayer = enemy.Attack;
                player.Health -= damageToPlayer;

                Console.WriteLine($"Enemy did {damageToPlayer} damage.");

                if (player.Health <= 0)
                {
                    Console.WriteLine("You died.");
                    Console.WriteLine("Game over.");
                    Console.ReadKey();
                    return "LOSE";
                }
                Console.ReadKey();
            }

            if (battleChoice == "R")
            {
                return "RUN";
            }
        }
        return "EXIT";
    }
}