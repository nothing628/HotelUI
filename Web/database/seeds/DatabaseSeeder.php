<?php

use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $this->call(BookingSeeder::class);
        $this->call(RoomSeeder::class);
        $this->call(LedgerSeeder::class);
    }
}
