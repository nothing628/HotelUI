<template>
    <v-container fluid grid-list-md>
        <v-layout row>
            <v-flex md8>
                <v-card>
                    <v-card-title>
                        <h2 class="card-title mb-0">Price Calendar</h2>
                    </v-card-title>
                    <v-card-title>
                        <v-date-picker v-model="date2"
                                       landscape
                                       :color="functionEventsColor(date2)"
                                       :event-color="functionEventsColor"
                                       :events="functionEvents"></v-date-picker>
                    </v-card-title>
                </v-card>
            </v-flex>
            <v-flex md4>
                <v-card>
                    <v-card-title>
                        <h2 class="card-title mb-0">Price Plan</h2>
                    </v-card-title>

                    <v-layout row>
                        <v-flex md12>
                            <v-btn color="primary" @click="addType"><v-icon>edit</v-icon> Add</v-btn>
                        </v-flex>
                    </v-layout>
                    
                    <table class="datatable table">
                        <thead>
                            <tr>
                                <td>Plan Type</td>
                                <td>Color</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="col,i in colors" :active="functionType(date2) == i" class="hand-cursor">
                                <td @click="changeType(date2, i)">{{ i }}</td>
                                <td @click="chooseColor(i)" :class="col"></td>
                            </tr>
                        </tbody>
                    </table>
                </v-card>
                <color-dialog @colorChanged="changeColor"></color-dialog>
                <v-snackbar :timeout="1000"
                            :top="true"
                            :color="snackbar.color"
                            v-model="snackbar.show">
                    {{ snackbar.text }}
                </v-snackbar>
            </v-flex>
        </v-layout>
    </v-container>
</template>
<style>
    .theme--light .table tbody tr[active] {
        background: #c0c0c0;
    }
    .hand-cursor {
        cursor: pointer;
    }
</style>
<script>
    import axios from 'axios'
    import moment from 'moment'
    import colorDialog from '../../components/dialog/ColorDialog'

    export default {
        components: {
            'color-dialog': colorDialog,
        },
        data() {
            return {
                date2: null,
                tmpType: '',
                colors: {},
                items: {},
                snackbar: {
                    color: 'success',
                    text: '',
                    show: false,
                },
            }
        },
        methods: {
            addType() {
                console.log('add type')
            },
            chooseColor(type) {
                this.tmpType = type
                this.$bus.$emit('choose-dialog')
            },
            changeColor(newcolor) {
                let data = {
                    type: this.tmpType,
                    color: newcolor,
                }
                
                axios.post('http://localhost.com/room/post/setColor', data).then(this.changeSuccess)
            },
            changeType(date, typ) {
                let dateForm = {
                    type: typ,
                    date: date,
                }

                axios.post('http://localhost.com/room/post/setDayEffect', dateForm).then(this.changeSuccess)
            },
            changeSuccess(response) {
                var data = response.data

                if (data.success) {
                    this.snackbar.color = 'success'
                } else {
                    this.snackbar.color = 'error'
                }

                this.snackbar.text = data.message
                this.snackbar.show = true
                this.getColorsApi()
            },
            functionType(date) {
                return this.items[date]
            },
            functionEventsColor(date) {
                let typ = this.functionType(date)
                let col = this.colors[typ]

                return col
            },
            functionEvents(date) {
                return true
            },
            getColorsApi() {
                axios.post('http://localhost.com/room/post/getDayEffect').then(this.getColorsData).catch(e => { })
            },
            getColorsData(response) {
                var data = response.data

                this.colors = data.colors
                this.items = data.items
            },
        },
        mounted() {
            this.date2 = moment().format('YYYY-MM-DD')
            this.getColorsApi()
        }
    }
</script>
