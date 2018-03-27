<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class CreateInvoiceTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('invoice', function (Blueprint $table) {
            $table->string('id', 25)->primary();
            $table->string('id_checkin', 25);
            $table->bigInteger('id_guest')->unsigned();
            $table->boolean('is_closed')->default(false);
            $table->timestamps();
        });

        Schema::create('invoice_detail', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->string('id_invoce', 25);
            $table->string('description')->nullable();
            $table->boolean('is_system')->default(false);
            $table->date('transaction_at');
            $table->decimal('ammount_in')->unsigned()->default(0);
            $table->decimal('ammount_out')->unsigned()->default(0);
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
        Schema::dropIfExists('invoice_detail');
        Schema::dropIfExists('invoice');
    }
}
