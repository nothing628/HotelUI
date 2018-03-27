<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateRoomTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('room_status', function (Blueprint $table) {
            $table->integer('id')->unsigned()->primary();
            $table->string('status', 50);
            $table->string('status_color', 6);
            $table->timestamps();
        });

        Schema::create('room_category', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->string('category', 50);
            $table->string('description', 200)->nullable();
            $table->timestamps();
        });

        Schema::create('room', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->bigInteger('id_category')->unsigned();
            $table->integer('id_status')->unsigned()->default(0);
            $table->string('room_number', 15)->unique();
            $table->integer('room_floor')->unsigned()->default(1);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('room');
        Schema::dropIfExists('room_category');
        Schema::dropIfExists('room_status');
    }
}
