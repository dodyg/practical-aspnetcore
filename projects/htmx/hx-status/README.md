# hx-status

Use `hx-status:422` and `hx-status:5xx` to choose swap behavior per response status. 

Exact codes win over `50x`, which wins over `5xx`. 

Available options include `swap`, `target`, `select`, `push`, `replace`, and `transition`.
