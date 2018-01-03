<template>
    <div class="picker picker--date card" data-ripple="false" style="height: auto;">
        <div class="picker__body">
            <div class="picker--date__header">
                <div class="picker--date__header-selector">
                    <v-btn flat icon @click="prevMonth">
                        <v-icon>chevron_left</v-icon>
                    </v-btn>
                    <div class="picker--date__header-selector-date">
                        <strong class="green--text text--lighten-1">{{ displayMonth }}</strong>
                    </div>
                    <v-btn flat icon @click="nextMonth">
                        <v-icon>chevron_right</v-icon>
                    </v-btn>
                </div>
            </div>
            <div class="picker--date__table">
                <table>
                    <thead>
                        <tr>
                            <th v-for="(d,idx) in weekday" v-key="idx">{{ d }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="i in getRows()">
                            <td v-for="j in 7">
                                <button v-if="getValid(i, j)" :style="getStyle(i, j)" type="button" :class="getClass(i, j)">
                                    <span class="btn__content">{{ getDateVal(i, j) }}</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>
<script>
    import moment from 'moment'

    export default {
        data() {
            return {
                weekday: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
                currMonth: 0,
                currYear: 0,
            }
        },
        computed: {
            today() {
                return new Date()
            },
            currDat() {
                return new Date(this.currYear, this.currMonth)
            },
            displayMonth() {
                var momen = moment(this.currDat)

                return momen.format("MMMM YYYY")
            }
        },
        props: {
            colors: { type: Object, default() { return {} } },
            items: { type: Object, default() { return {} } }
        },
        methods: {
            nextMonth() {
                var momen = moment(this.currDat)
                var res = momen.add(1, 'M')
                
                this.currYear = res.year()
                this.currMonth = res.month()
            },
            prevMonth() {
                var momen = moment(this.currDat)
                var res = momen.subtract(1, 'M')

                this.currYear = res.year()
                this.currMonth = res.month()
            },
            getRows() {
                var momen = moment(this.currDat)
                var start = momen.startOf('M')
                var end = momen.endOf('M')
                var offset = start.day()
                var enddate = end.date() + offset
                var ret = Math.ceil(enddate / 7)

                return ret == 0 ? 1 : ret
            },
            getOffset(date) {
                var momen = moment(date)

                return momen.day()
            },
            getDateVal(row, col) {
                var momen = moment(this.currDat)
                var start = momen.startOf('M')
                var off = start.day()
                var calc = (row - 1) * 7 + col - off

                return calc
            },
            getValid(row, col) {
                var momen = moment(this.currDat)
                var end = momen.endOf('M')
                var endDate = end.date()
                var date = this.getDateVal(row, col)

                return date > 0 && date <= endDate 
            },
            getClass(r, c) {
                var base = ['btn', 'btn--raised', 'btn--icon', 'waves-effect']

                if (this.getValid(r, c)) {
                    var date = this.getDateVal(r, c)
                    var selDate = moment(this.currDat).date(date)

                    if (this.isMarked(selDate.format("YYYY-MM-DD"))) {
                        base = base.concat(['btn--outline', 'btn--depressed'])
                    }
                }

                return base
            },
            getStyle(r, c) {
                if (this.getValid(r, c)) {
                    var date = this.getDateVal(r, c)
                    var selDate = moment(this.currDat).date(date)
                    var style = this.getColorStyle(selDate.format("YYYY-MM-DD"))
                    
                    return style
                }

                return {}
            },
            getColorStyle(date) {
                if (date in this.items) {
                    var item = this.items[date]

                    if (item in this.colors) {
                        var color = this.colors[item]

                        return {'border-color' : color, 'color' : color}
                    }
                }

                return {}
            },
            isMarked(date) {
                return date in this.items
            }
        },
        mounted() {
            var today = moment()

            this.currMonth = today.month()
            this.currYear = today.year()
        }
    }
</script>
