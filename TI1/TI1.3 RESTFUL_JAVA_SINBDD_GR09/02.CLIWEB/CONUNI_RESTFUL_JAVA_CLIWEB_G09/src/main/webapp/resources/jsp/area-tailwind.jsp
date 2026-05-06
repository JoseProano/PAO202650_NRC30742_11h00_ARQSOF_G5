<div class="w-full max-w-4xl rounded-xl bg-white border border-gray-200 p-6 sm:p-8 flex flex-col gap-6 shadow-xl shadow-gray-200/50">
  <div class="flex flex-col md:flex-row gap-4">
    <div class="flex-1"><label class="block text-sm font-medium text-slate-500 pb-2" for="areaAmount">Cantidad</label>
      <input id="areaAmount" class="form-input w-full rounded-lg border border-gray-300 bg-gray-50 h-14 p-4" type="number" step="0.01" placeholder="1" />
    </div>
    <div class="flex-1"><label class="block text-sm font-medium text-slate-500 pb-2" for="areaFrom">De</label>
      <select id="areaFrom" class="form-input w-full rounded-lg border border-gray-300 bg-gray-50 h-14 p-4">
        <option>Metros cuadrados</option><option>Pies cuadrados</option><option>Hectáreas</option><option>Acres</option>
      </select></div>
    <div class="flex-1"><label class="block text-sm font-medium text-slate-500 pb-2" for="areaTo">A</label>
      <select id="areaTo" class="form-input w-full rounded-lg border border-gray-300 bg-gray-50 h-14 p-4">
        <option>Pies cuadrados</option><option>Metros cuadrados</option><option>Acres</option><option>Hectáreas</option>
      </select></div>
  </div>
  <button class="flex w-full items-center justify-center rounded-lg h-12 px-6 text-white font-bold bg-[#afe0f8] text-slate-800 hover:opacity-90"
          onclick="convertPanel('area','areaAmount','areaFrom','areaTo','areaResult')">Convertir</button>
  <div class="w-full rounded-lg bg-[#afe0f8]/20 p-6 text-center">
    <p class="text-slate-600 text-lg">Valor convertido</p>
    <p id="areaResult" class="text-sky-600 text-4xl font-bold mt-2">--</p>
  </div>
</div>



