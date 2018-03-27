<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateGuestTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('guest', function (Blueprint $table) {
            $table->increments('id');
            $table->string('id_number', 50);
            $table->string('id_kind', 20)->default('ID Number');
            $table->string('fullname', 50);
            $table->string('email')->unqiue();
            $table->boolean('is_vip')->default(false);
            $table->string('birth_place', 50)->nullable();
            $table->date('birth_date');
            $table->string('phone1', 15);
            $table->string('phone2', 15)->nullable();
            $table->string('address')->nullable();
            $table->string('city', 50)->nullable();
            $table->string('province', 50)->nullable();
            $table->string('postcode', 10)->nullalbe();
            $table->string('photo_doc', 50);
            $table->string('photo_guest', 50)->nullable();
            $table->timestamps();
            $table->unique(['id_number', 'id_kind']);
        });

        Schema::create('booking_type', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->string('type', 50);
            $table->boolean('is_online')->default(false);
            $table->string('online_provider', 50)->nullable();
            $table->timestamps();
        });

        Schema::create('booking', function (Blueprint $table) {
            $table->string('id', 25)->primary();
            $table->bigInteger('id_guest')->unsigned();
            $table->bigInteger('id_type')->unsigned();
            $table->bigInteger('id_room')->unsigned();
            $table->string('id_checkin', 25)->nullable();
            $table->boolean('is_checkin')->default(false);
            $table->integer('count_child')->unsigned()->default(0);
            $table->integer('count_adult')->unsigned()->default(1);
            $table->date('arrive_at');
            $table->date('departure_at');
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
        Schema::dropIfExists('booking');
        Schema::dropIfExists('booking_type');
        Schema::dropIfExists('guest');
    }
}
