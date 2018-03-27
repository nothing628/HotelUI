<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreatePriceTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('dayeffect', function (Blueprint $table) {
            $table->increments('id');
            $table->string('effect', 50)->unique();
            $table->string('color', 25);
            $table->boolean('can_delete')->default(true);
            $table->timestamps();
        });

        Schema::create('daycycle', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->integer('id_effect')->unsigned();
            $table->date('date_at')->unique();
            $table->timestamps();
        });

        Schema::create('room_price', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('id_category')->unsigned();
            $table->integer('id_effect')->unsigned();
            $table->decimal('price')->unsigned()->default(0);
            $table->timestamps();
            $table->unique(['id_category', 'id_effect']);
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('room_price');
        Schema::dropIfExists('daycycle');
        Schema::dropIfExists('dayeffect');
    }
}
