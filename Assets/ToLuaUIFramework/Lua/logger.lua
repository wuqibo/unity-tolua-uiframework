local function printTable(t, method, prefix)
    local printContent = ""
    local print_t_cache = {}
    local function sub_tablePrint(t, indent)
        if (print_t_cache[tostring(t)]) then
            printContent = printContent .. (indent .. "*" .. tostring(t))
        else
            print_t_cache[tostring(t)] = true
            if (type(t) == "table") then
                for pos, val in pairs(t) do
                    if (type(val) == "table") then
                        printContent = printContent .. (indent .. "[" .. pos .. "] => " .. tostring(t) .. " {")
                        sub_tablePrint(val, indent .. string.rep(" ", string.len(pos) + 8))
                        printContent = printContent .. (indent .. string.rep(" ", string.len(pos) + 6) .. "}")
                    elseif (type(val) == "string") then
                        printContent = printContent .. (indent .. "[" .. pos .. '] => "' .. val .. '"')
                    else
                        printContent = printContent .. (indent .. "[" .. pos .. "] => " .. tostring(val))
                    end
                end
            else
                printContent = printContent .. (indent .. tostring(t))
            end
        end
    end
    if (type(t) == "table") then
        printContent = printContent .. (tostring(t) .. " {")
        sub_tablePrint(t, "  ")
        printContent = printContent .. ("}")
    else
        sub_tablePrint(t, "  ")
    end
    if prefix then
        printContent = "WQB-> " .. prefix .. " " .. printContent .. "\n" .. debug.traceback()
    else
        printContent = "WQB-> " .. printContent .. "\n" .. debug.traceback()
    end

    if method == 1 then
        Debugger.Log(printContent)
    elseif method == 2 then
        Debugger.LogWarning(printContent)
    else
        Debugger.LogError(printContent)
    end
end

function Log(param1, param2)
    if param2 then
        printTable(param2, 1, param1)
    else
        printTable(param1, 1, nil)
    end
end

function LogWarning(param1, param2)
    if param2 then
        printTable(param2, 2, param1)
    else
        printTable(param1, 2, nil)
    end
end

function LogError(param1, param2)
    if param2 then
        printTable(param2, 3, param1)
    else
        printTable(param1, 3, nil)
    end
end
