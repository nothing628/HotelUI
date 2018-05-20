<template>
    <ul class="navigation">
        <li v-for="menu in list_menu" :key="menu.html" :class="getClassMenu(menu)">
            <a :href="menu.url || ''" v-html="menu.html"><i class="zmdi zmdi-home"></i> Home</a>

            <ul v-if="menu.submenu">
                <li v-for="sub in menu.submenu" :key="sub.html" class="@@active" :class="getClassMenu(sub)">
                    <a :href="sub.url || ''" v-html="sub.html"></a>
                </li>
            </ul>
        </li>
    </ul>
</template>
<style>
.navigation__sub .navigation__active:before {
    content: "";
}
</style>
<script>
    export default {
        data() {
            return {
                list_menu: []
            }
        },
        methods: {
            getClassMenu(menu) {
                let res = []
                let innurl = []
                let current = window.location.href

                if (menu.submenu) {
                    res.push('navigation__sub')
                    res.push('@@widgetactive')

                    menu.submenu.forEach(x => innurl.push(x.url))
                }
                
                if (current == menu.url || innurl.findIndex(c => c == current) > -1) {
                    res.push('navigation__active')
                }

                return res
            }
        },
        mounted() {
            menu_list.forEach(x => this.list_menu.push(x))
        }
    }
</script>