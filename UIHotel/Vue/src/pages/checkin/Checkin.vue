<template>
    <v-app id="inspire">
        <v-card>
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Registration Info</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <v-form v-model="registration.valid">
                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Booking Number</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="text"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-btn fab dark small color="primary">
                                <v-icon dark>search</v-icon>
                            </v-btn>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Deposit</v-subheader>
                        </v-flex>
                        <v-flex md3>
                            <v-text-field type="number" prefix="Rp"></v-text-field>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Arrival Date</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-dialog persistent
                                      v-model="modal1"
                                      lazy
                                      full-width
                                      width="290px">
                                <v-text-field slot="activator"
                                              v-model="date1"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="date1" scrollable actions>
                                    <template slot-scope="{ save, cancel }">
                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                            <v-btn flat color="primary" @click="save">OK</v-btn>
                                        </v-card-actions>
                                    </template>
                                </v-date-picker>
                            </v-dialog>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Departure Date</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-dialog persistent
                                      v-model="modal2"
                                      lazy
                                      full-width
                                      width="290px">
                                <v-text-field slot="activator"
                                              v-model="date2"
                                              prepend-icon="event"
                                              readonly></v-text-field>
                                <v-date-picker v-model="date2" scrollable actions>
                                    <template slot-scope="{ save, cancel }">
                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-btn flat color="primary" @click="cancel">Cancel</v-btn>
                                            <v-btn flat color="primary" @click="save">OK</v-btn>
                                        </v-card-actions>
                                    </template>
                                </v-date-picker>
                            </v-dialog>
                        </v-flex>
                    </v-layout>

                    <v-layout row>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Jumlah Adult</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="number" value="1"></v-text-field>
                        </v-flex>
                        <v-flex md2>
                            <v-subheader class="text--lighten-1">Jumlah Child</v-subheader>
                        </v-flex>
                        <v-flex md4>
                            <v-text-field type="number" value="0"></v-text-field>
                        </v-flex>
                    </v-layout>
                </v-form>
            </div>
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Room Select</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <form class="form-row">
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Room Type</label>
                        <div class="col-6">
                            <input type="text" class="form-control" placeholder="Select room" />
                        </div>
                        <div class="col-2">
                            <button class="btn btn-lg btn--icon btn-primary" data-toggle="modal" data-target="#modal-default" type="button"><i class="zmdi zmdi-search"></i></button>
                        </div>
                    </div>

                    <div class="form-group row">
                        <label class="col-2 col-form-label">Note</label>
                        <div class="col-6">
                            <textarea class="form-control" rows="2"></textarea>
                        </div>
                    </div>
                </form>
            </div>
            <v-card-title primary-title>
                <div>
                    <h2 class="card-title mb-0">Guest Info</h2>
                </div>
            </v-card-title>
            <div class="card-block">
                <form class="form-row">
                    <div class="form-group row">
                        <label class="col-2 col-form-label">ID Number*</label>
                        <div class="col-6">
                            <input class="form-control" placeholder="ID Number" type="text" required>
                        </div>
                        <div class="col-1">
                            <button class="btn btn-lg btn--icon btn-primary" type="button"><i class="zmdi zmdi-search"></i></button>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Name*</label>
                        <div class="col-8">
                            <input class="form-control" placeholder="Fullname" type="text" required>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Guest Type</label>
                        <div class="col-6">
                            <label class="custom-control custom-radio">
                                <input name="radio-inline" class="custom-control-input" type="radio">
                                <span class="custom-control-indicator"></span>
                                <span class="custom-control-description">Regular</span>
                            </label>

                            <label class="custom-control custom-radio">
                                <input name="radio-inline" class="custom-control-input" type="radio">
                                <span class="custom-control-indicator"></span>
                                <span class="custom-control-description">VIP</span>
                            </label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Birth Day</label>
                        <div class="col-4">
                            <input class="form-control" placeholder="Birth place" type="text">
                        </div>
                        <div class="col-4">
                            <input class="form-control" value="2017-12-01" type="date">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Address</label>
                        <div class="col-6">
                            <textarea class="form-control" rows="2"></textarea>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">State</label>
                        <div class="col-4">
                            <select class="form-control">
                                <option>Select State</option>
                                <option>Indonesia</option>
                                <option>China</option>
                                <option>Korea Utara</option>
                                <option>Option 4</option>
                                <option>Option 5</option>
                            </select>
                        </div>
                        <label class="col-2 col-form-label">Province</label>
                        <div class="col-4">
                            <input class="form-control" placeholder="Province" type="text">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">City</label>
                        <div class="col-4">
                            <input class="form-control" placeholder="City" type="text">
                        </div>
                        <label class="col-2 col-form-label">Pos Code</label>
                        <div class="col-4">
                            <input class="form-control" placeholder="Pos Code" type="text">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Email</label>
                        <div class="col-6">
                            <input class="form-control" placeholder="Email" type="text">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Phone Number 1*</label>
                        <div class="col-4">
                            <input class="form-control" placeholder="Phone Number 1" type="text" required>
                        </div>
                        <label class="col-2 col-form-label">Phone Number 2</label>
                        <div class="col-4">
                            <input class="form-control" placeholder="Phone Number 2" type="text">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Photo Document*</label>
                        <div class="col-6">
                            <button class="btn btn-lg btn-primary btn--icon" type="button"><i class="zmdi zmdi-cloud-upload"></i></button>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-2 col-form-label">Photo Guest</label>
                        <div class="col-6">
                            <button class="btn btn-lg btn-primary btn--icon" type="button"><i class="zmdi zmdi-cloud-upload"></i></button>
                        </div>
                    </div>

                    <div class="form-group row">
                        <button type="button" class="btn btn-lg btn--icon-text waves-effect error">Cancel</button>
                        <button type="button" class="btn btn-lg btn--icon-text waves-effect primary">Checkin <i class="zmdi zmdi-check-all"></i></button>
                    </div>
                </form>
            </div>
        </v-card>
    </v-app>
</template>
<script>
    export default {
        data() {
            return {
                date1: null,
                date2: null,
                modal1: false,
                modal2: false,
                registration : {
                    valid: true,
                    book_no: "",
                }
            }
        }
    }
</script>
