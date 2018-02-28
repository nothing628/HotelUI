<template>
    <v-layout row>
        <v-dialog v-model="modal" max-width="600px">
            <v-card>
                <v-card-title><strong>{{ title }}</strong></v-card-title>
                <v-card-text>
                    <v-container fluid class="px-3">
                        <v-layout row wrap>
                            <v-flex xs12>
                                <v-btn fab>
                                    <v-icon :color="color">{{ icon }}</v-icon>
                                </v-btn>
                                <v-btn>Change Icon</v-btn>
                                <v-btn @click="showDialogColor">Change Color</v-btn>
                            </v-flex>
                            <v-flex xs12>
                                <v-text-field label="Category"
                                              :rules="rules.category"
                                              :counter="50"
                                              rows="2"
                                              ref="category"
                                              v-model="category"></v-text-field>
                            </v-flex>
                        </v-layout>
                    </v-container>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions>
                    <v-btn color="red darken-1" dark @click.native="cancel">Cancel</v-btn>
                    <v-btn color="blue darken-1" dark @click.native="save">OK</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <tcolor @colorChanged="changeColor"></tcolor>
    </v-layout>
</template>
<script>
    import tcolor from '../../components/dialog/ColorDialog.vue'
    export default {
        components: {
            tcolor
        },
        data() {
            return {
                modal: false,
                title: 'New Category',
                category: '',
                icon: 'clear',
                color: 'red lighten-1',
                validref: ['category'],
                rules: {
                    category: [
                        (v) => (!!v && v.length > 0) || 'This field is required',
                        (v) => (!!v && v.length <= 50) || 'Max 50 characters'
                    ]
                }
            }
        },
        methods: {
            showDialogColor() {
                this.$bus.$emit('choose-color')
            },
            showDialogIcon() {
                //
            },
            showDialog() {
                this.title = 'New Category'
                this.modal = true
                this.resetValidation()
            },
            editDialog(item) {
                this.title = 'Edit Category'
                this.modal = true
                this.resetValidation()
            },
            resetValidation() {
                let keys = this.validref

                keys.forEach(x => {
                    this.$refs[x].reset()
                })
            },
            resetData() {
                //
            },
            save() {
                //
            },
            cancel() {
                this.modal = false
            },
            changeColor(data) {
                this.color = data
            }
        },
        mounted() {
            this.$bus.$on('new-category', this.showDialog)
            this.$bus.$on('edit-category', this.editDialog)
            this.resetValidation()
            this.resetData()
        },
        beforeDestroy() {
            this.$bus.$off('new-category', this.showDialog)
            this.$bus.$off('edit-category', this.editDialog)
        }
    }
</script>
