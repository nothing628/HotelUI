<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateLedgerTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('ledger_category', function (Blueprint $table) {
            $table->increments('id');
            $table->string('description', 50);
            $table->string('icon', 20)->nullable();
            $table->string('color', 20)->nullable();
            $table->boolean('is_expense')->default(false);
            $table->timestamps();
        });
        
        Schema::create('ledger_log', function (Blueprint $table) {
            $table->string('id', 16)->primary();
            $table->integer('id_category')->unsigned();
            $table->string('description');
            $table->decimal('ammount_in')->unsigned()->default(0);
            $table->decimal('ammount_out')->unsigned()->default(0);
            $table->boolean('is_close')->default(false);
            $table->datetime('transaction_at');
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
        Schema::dropIfExists('ledger_log');
        Schema::dropIfExists('ledger_category');
    }
}
