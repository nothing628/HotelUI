<template>
    <v-layout row>
        <v-dialog v-model="modal" max-width="600px">
            <v-card>
                <v-card-title><strong>{{ title }}</strong></v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-container fluid class="px-3">
                        <v-layout row wrap>
                            <v-flex xs12>
                                <v-btn fab>
                                    <v-icon :color="color">{{ icon }}</v-icon>
                                </v-btn>
                                <v-btn @click="showDialogIcon">Change Icon</v-btn>
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
                            <v-flex xs12>
                                <v-switch label="Outcome Category"
                                          v-model="isOutcome"></v-switch>
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
        <tcolor @save="changeColor" @cancel="modalColor = false" :show-modal.sync="modalColor"></tcolor>
        <ticon @save="changeIcon" @cancel="modalIcon = false" :show-modal.sync="modalIcon"></ticon>
    </v-layout>
</template>
<script>
    import tcolor from '../../components/dialog/ColorDialog.vue'
    import ticon from '../../components/dialog/IconDialog.vue'
    export default {
        components: {
            tcolor,
            ticon,
        },
        data() {
            return {
                modal: false,
                modalIcon: false,
                modalColor: false,
                id: '',
                category: '',
                isOutcome: false,
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
        props: {
            dialogName: { type: String, default: null },
            title: { type: String, default: 'New Category' }
        },
        methods: {
            showDialogColor() {
                this.modalColor = true
            },
            showDialogIcon() {
                this.modalIcon = true
            },
            showDialog(param) {
                if (param.name != this.dialogName)
                    return

                this.modal = true
                this.resetValidation()
                this.resetData()

                //Set Data if the param is exists
                this.color = param.Color || ''
                this.icon = param.Icon || ''
                this.category = param.Description || ''
                this.isOutcome = param.IsExpense || false
                this.id = param.Id || ''
            },
            resetValidation() {
                let keys = this.validref

                keys.forEach(x => {
                    this.$refs[x].reset()
                })
            },
            resetData() {
                this.category = ''
            },
            save() {
                let data = {
                    Id: this.id,
                    Icon: this.icon,
                    Color: this.color,
                    IsExpense: this.isOutcome,
                    Description: this.category,
                }

                this.$emit('save', data)
                this.modal = false
            },
            cancel() {
                this.modal = false
            },
            changeColor(data) {
                this.modalColor = false
                this.color = data
            },
            changeIcon(data) {
                this.modalIcon = false
                this.icon = data
            }
        },
        mounted() {
            this.$bus.$on('new-category', this.showDialog)
            this.resetValidation()
            this.resetData()
        },
        beforeDestroy() {
            this.$bus.$off('new-category', this.showDialog)
        }
    }
</script>
