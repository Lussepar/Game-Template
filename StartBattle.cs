class Battle
{
    public void BattleStats(Player player, Enemy enemy)
    {
        Console.WriteLine($"Your HP: {player.PlayerHealth}");
        Console.WriteLine($"Enemy HP: {enemy.EnemyHP}");
    }
    public string StartBattle(Player player, Enemy enemy)
    {
        while (player.IsAlive && enemy.IsAlive)
        {
            Console.Clear();

            BattleStats(player, enemy);

            Console.WriteLine(" [A] for Attack");
            Console.WriteLine(" [R] for Run");
            Console.WriteLine("=============");
            string battleChoice = Console.ReadLine().ToUpper();

            if (battleChoice == "A")
            {
                int damageToEnemy = player.PlayerAttack;

                enemy.EnemyHP -= damageToEnemy;
                Console.WriteLine($"You did {damageToEnemy} damage.");
                if (enemy.EnemyHP <= 0)
                {
                    Console.WriteLine("You are victorious!");
                    Console.ReadKey();
                    return "WIN";
                }

                Console.ReadKey();

                int damageToPlayer = enemy.EnemyAttack;

                player.PlayerHealth -= damageToPlayer;
                Console.WriteLine($"Enemy did {damageToPlayer} damage.");
                if (player.PlayerHealth <= 0)
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