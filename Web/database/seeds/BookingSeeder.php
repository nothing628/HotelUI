<?php

use Illuminate\Database\Seeder;
use App\Models\BookingType;

class BookingSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        BookingType::create(['type' => 'Walk-In']);
        BookingType::create(['type' => 'Telephone']);
        BookingType::create(['type' => 'Traveloka', 'is_online' => true, 'online_provider' => 'Traveloka']);
        BookingType::create(['type' => 'Agoda', 'is_online' => true, 'online_provider' => 'Agoda']);
    }
}
