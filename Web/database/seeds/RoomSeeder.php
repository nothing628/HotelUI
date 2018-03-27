<?php

use Illuminate\Database\Seeder;
use App\Models\RoomStatus;
use App\Models\RoomCategory;
use App\Models\Room;

class RoomSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        RoomCategory::create(['category' => 'Big']);
        RoomCategory::create(['category' => 'Medium']);
        RoomCategory::create(['category' => 'Small']);
        RoomStatus::create(['id' => 1, 'status' => 'Vacant', 'status_color' => '32C787']);
        RoomStatus::create(['id' => 2, 'status' => 'Booked', 'status_color' => 'FF9800']);
        RoomStatus::create(['id' => 3, 'status' => 'Occupied', 'status_color' => 'FF5652']);
        RoomStatus::create(['id' => 4, 'status' => 'Cleaning', 'status_color' => 'AB47BC']);
        RoomStatus::create(['id' => 5, 'status' => 'Maintance', 'status_color' => '757575']);
        RoomStatus::create(['id' => 6, 'status' => 'Late Checkout', 'status_color' => '2196F3']);
        Room::create(['id_category' => 1, 'id_status' => 1, 'room_number' => '201']);
        Room::create(['id_category' => 1, 'id_status' => 1, 'room_number' => '202']);
        Room::create(['id_category' => 2, 'id_status' => 1, 'room_number' => '301']);
        Room::create(['id_category' => 2, 'id_status' => 1, 'room_number' => '302']);
        Room::create(['id_category' => 3, 'id_status' => 1, 'room_number' => '401']);
        Room::create(['id_category' => 3, 'id_status' => 1, 'room_number' => '402']);
    }
}
