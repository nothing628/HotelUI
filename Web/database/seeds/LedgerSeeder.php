<?php

use Illuminate\Database\Seeder;
use App\Models\LedgerCategory;

class LedgerSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        LedgerCategory::create(['description' => 'Cash', 'icon' => 'local_atm', 'color' => 'light-green darken-3', 'is_expense' => false]);
        LedgerCategory::create(['description' => 'Income', 'icon' => 'get_app', 'color' => 'blue darken-3', 'is_expense' => false]);
        LedgerCategory::create(['description' => 'Salary', 'icon' => 'group', 'color' => 'yellow darken-4', 'is_expense' => false]);
        LedgerCategory::create(['description' => 'Food & Drinks', 'icon' => 'restaurant', 'color' => 'cyan darken-3', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Transportation', 'icon' => 'local_airport', 'color' => 'deep-orange darken-4', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Comunication', 'icon' => 'local_phone', 'color' => 'deep-purple darken-4', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Tax', 'icon' => 'public', 'color' => 'green darken-4', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Utilities', 'icon' => 'build', 'color' => 'amber darken-3', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Insurance', 'icon' => 'business_center', 'color' => 'brown darken-4', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Loan', 'icon' => 'card_giftcard', 'color' => 'cyan darken-2', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Uncategorized', 'icon' => 'all_inclusive', 'color' => 'black', 'is_expense' => true]);
        LedgerCategory::create(['description' => 'Uncategorized', 'icon' => 'all_inclusive', 'color' => 'black', 'is_expense' => true]);
    }
}
